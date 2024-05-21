using data_layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

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

            // Wypełnianie grid_reference
            for (int x = 0; x <= gridWidth; x++) {
                for (int y = 0; y <= gridHeight; y++) {
                    grid_reference.Add((x, y));
                }
            }

            // shuffle grid_reference by Fisher-Yates shuffle
            int n = grid_reference.Count;
            for (int i = n - 1; i > 0; i--) {
                int j = random.Next(i + 1);
                (double, double) temp = grid_reference[i];
                grid_reference[i] = grid_reference[j];
                grid_reference[j] = temp;
            }

            int colorIndex = 0;
            for (int i = 0; i < amount; i++) {
                double x = grid_reference[i].Item1 * radius;
                double y = grid_reference[i].Item2 * radius;
                double xSpeed = random.NextDouble() * 4 - 2; // Prędkość w zakresie [-2, 2]
                double ySpeed = random.NextDouble() * 4 - 2; // Prędkość w zakresie [-2, 2]

                Ball ball;
                if ((i % 15) + 1 == 8) {
                    ball = new Ball(
                        radius,
                        x,
                        y,
                        xSpeed,
                        ySpeed,
                        i,
                        "#010001",
                        5 // do zmiany
                    );
                } else {
                    ball = new Ball(
                        radius,
                        x,
                        y,
                        xSpeed,
                        ySpeed,
                        i,
                        ballColors[colorIndex],
                        5 // do zmiany
                    );
                }

                _Ball_Repository.AddBall(ball);
                ball.OnChange += UpdateBall;

                colorIndex++;
                if (colorIndex > 14) {
                    colorIndex = 0;
                }
            }
        }

        public void ClearAllBalls() {
            foreach (Ball ball in GetAllBalls()) {
                ball.isMoving = false;
                ball.OnChange -= UpdateBall;
            }
            _Ball_Repository.ClearAllBalls();
        }

        public List<Ball> GetAllBalls() {
            return _Ball_Repository.GetAllBalls();
        }

        public void UpdateBall(IBall ball) {
            lock (_Lock) {
                // wall collision
                double new_x_position = ball.X_position + ball.X_velocity;
                double new_y_position = ball.Y_position + ball.Y_velocity;

                if (new_x_position <= 0) {
                    ball.X_position = 0;
                    ball.X_velocity *= -1.0;
                } else if (new_x_position + ball.Radius >= _Width) {
                    ball.X_position = _Width - ball.Radius;
                    ball.X_velocity *= -1.0;
                } else {
                    ball.X_position = new_x_position;
                }

                if (new_y_position <= 0) {
                    ball.Y_position = 0;
                    ball.Y_velocity *= -1.0;
                } else if (new_y_position + ball.Radius >= _Height) {
                    ball.Y_position = _Height - ball.Radius;
                    ball.Y_velocity *= -1.0;
                } else {
                    ball.Y_position = new_y_position;
                }

                // collision with other balls
                foreach (IBall differentBall in GetAllBalls()) {
                    if (differentBall.ID == ball.ID) {
                        continue;
                    }

                    double distX = new_x_position - differentBall.X_position;
                    double distY = new_y_position - differentBall.Y_position;
                    // calculating distance between our ball and other ball
                    double distance = Math.Sqrt(distX * distX + distY * distY);

                    if (distance < ball.Radius / 2 + differentBall.Radius / 2) {
                        // calculating angle
                        double angle = Math.Atan2(distX, distY);

                        // calculating speed of balls
                        double ballVelocity = Math.Sqrt(ball.X_velocity * ball.X_velocity + ball.Y_velocity * ball.Y_velocity);
                        double diffBallVelocity = Math.Sqrt(differentBall.X_velocity * differentBall.X_velocity + differentBall.Y_velocity * differentBall.Y_velocity);

                        // magic of math
                        double newBallX_velocity =
                            (ballVelocity * (ball.Weight - differentBall.Weight) +
                             2 * differentBall.Weight * diffBallVelocity) / (ball.Weight + differentBall.Weight) *
                            Math.Cos(angle);

                        double newBallY_velocity =
                            (ballVelocity * (ball.Weight - differentBall.Weight) +
                             2 * differentBall.Weight * diffBallVelocity) / (ball.Weight + differentBall.Weight) *
                            Math.Sin(angle);

                        double newDifferentBallX_velocity =
                            (diffBallVelocity * (differentBall.Weight - ball.Weight) +
                             2 * ball.Weight * ballVelocity) / (ball.Weight + differentBall.Weight) *
                            Math.Cos(angle + Math.PI);

                        double newDifferentBallY_velocity =
                            (diffBallVelocity * (differentBall.Weight - ball.Weight) +
                             2 * ball.Weight * ballVelocity) / (ball.Weight + differentBall.Weight) *
                            Math.Sin(angle + Math.PI);

                        ball.X_velocity = newBallX_velocity;
                        ball.Y_velocity = newBallY_velocity;
                        differentBall.X_velocity = newDifferentBallX_velocity;
                        differentBall.Y_velocity = newDifferentBallY_velocity;
                    }
                }
            }
        }

        public void UpdateBalls() {
            Barrier barrier = new Barrier(GetAllBalls().Count, (b) => {
                OnChange?.Invoke();
                Thread.Sleep(10);
            });
            foreach (Ball ball in GetAllBalls()) {
                ball.NewThread(barrier);
            }
        }
    }
}
