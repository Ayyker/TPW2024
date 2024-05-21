using data_layer;
using System;
using System.Collections.Generic;

namespace logic_layer {

    public class BallManager : IBallManager {
        private int _Width;
        private int _Height;
        private IBallRepository _Ball_Repository;
        private Random random = new Random();
        private List<string> ballColors = new List<string>
        {
            "#ffd400",
            "#003db1",
            "#e71c01",
            "#4e029d",
            "#fa4c01",
            "#0f5c01",
            "#6c061a",
            "#ffd400",
            "#003db1",
            "#e71c01",
            "#4e029d",
            "#fa4c01",
            "#0f5c01",
            "#6c061a",
            "#fefedf"
        };

        private readonly object _Lock = new object();
        public event NotifyDelegateBallManager.NotifyBallManager? OnChange;

        public BallManager(int width, int height, IBallRepository ballRepository) {
            _Width = width;
            _Height = height;
            _Ball_Repository = ballRepository;
        }

        public int Width {
            get => _Width;
        }

        public int Height {
            get => _Height;
        }

        public void GenerateBalls(int amount) {
            double radius = 45.0;
            double gridHeight = (_Height - radius) / radius;
            double gridWidth = (Width - radius) / radius;
            List<(double, double)> grid_reference = new List<(double, double)>();
            
            // shuffle grid_reference by Fisher-Yates shuffle
            int n = grid_reference.Count;
            for (int i = n - 1; i > 0; i--) {
                int j = random.Next(i + 1);
                (double, double) temp = grid_reference[i];
                grid_reference[i] = grid_reference[j];
                grid_reference[j] = temp;
            }
            for (int x = 0; x <= gridWidth; x++) {
                for (int y = 0; y <= gridHeight; y++) {
                    grid_reference.Add((x, y));
                }
            }

            int colorIndex = 0;
            for (int i = 0; i < amount; i++) {
                if ((i % 15) + 1 == 8) {
                    Ball b1 = new Ball(
                        radius,
                        grid_reference[i].Item1 * radius,
                        grid_reference[i].Item2 * radius,
                        random.NextDouble() * 4,
                        random.NextDouble() * 4,
                        i,
                        "#010001",
                        5 // do zmiany
                    );

                    _Ball_Repository.AddBall(b1);
                    // b1.OnChange += UpdateBall;

                }
                else {
                    Ball b2 = new Ball(
                        radius,
                        grid_reference[i].Item1 * radius,
                        grid_reference[i].Item2 * radius,
                        random.NextDouble() * 4,
                        random.NextDouble() * 4,
                        i,
                        ballColors[colorIndex],
                        5 // do zmiany
                    );

                    _Ball_Repository.AddBall(b2);
                    // b2.OnChange += UpdateBall;
                }

                colorIndex++;
                if (colorIndex > 14) {
                    colorIndex = 0;
                }
            }
        }

        public void ClearAllBalls() {
            _Ball_Repository.ClearAllBalls();
        }

        public IReadOnlyList<Ball> GetAllBalls() {
            return _Ball_Repository.GetAllBalls();
        }
    }
}
