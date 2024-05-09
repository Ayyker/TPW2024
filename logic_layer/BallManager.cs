using data_layer;

namespace logic_layer
{
    public static class ShuffleExtensions {
        private static Random random = new Random();

        public static void Shuffle<T>(this IList<T> list) {
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    public class BallManager : IBallManager
    {
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
            "#010001",
            "#ffd400",
            "#003db1",
            "#e71c01",
            "#4e029d",
            "#fa4c01",
            "#0f5c01",
            "#6c061a",
            "#fefedf"
        };

        public BallManager(int width, int height, IBallRepository ballRepository)
        {

            _Width = width;
            _Height = height;
            _Ball_Repository = ballRepository;
        }
        public int Width
        {
            get => _Width;
        }

        public int Height
        {
            get => _Height;
        }

        public void GenerateBalls(int amount)
        {
            // hard coded value of ball radius
            double radius = 45.0;
            //ballColors.Shuffle();
            int colorIndex = 0;
            for (int i = 0; i < amount; i++)
            {
                _Ball_Repository.AddBall(
                    new Ball(
                        radius,
                        random.NextDouble() * (_Width - radius),
                        random.NextDouble() * (_Height - radius),
                        random.NextDouble() * 4,
                        random.NextDouble() * 4,
                        i,
                        ballColors[colorIndex]
                        )
                    );
                colorIndex++;
                if ( colorIndex > 15) {
                    colorIndex = 0;
                }
            }
        }
        public void UpdateBalls()
        {
            foreach (Ball ball in _Ball_Repository.GetAllBalls())
            {
                double new_x_position = ball.X_position + ball.X_velocity;
                double new_y_position = ball.Y_position + ball.Y_velocity;

                if (new_x_position <= 0)
                {
                    ball.X_position = 0;
                    ball.X_velocity *= -1.0;
                }
                else if (new_x_position + ball.Radius >= _Width)
                {
                    ball.X_position = _Width - ball.Radius;
                    ball.X_velocity *= -1.0;
                }
                else
                {
                    ball.X_position = new_x_position;
                }

                if (new_y_position <= 0)
                {
                    ball.Y_position = 0;
                    ball.Y_velocity *= -1.0;
                }
                else if (new_y_position + ball.Radius >= _Height)
                {
                    ball.Y_position = _Height - ball.Radius;
                    ball.Y_velocity *= -1.0;
                }
                else
                {
                    ball.Y_position = new_y_position;
                }
            }
        }

        public void ClearAllBalls()
        {
            _Ball_Repository.ClearAllBalls();
        }
        public IReadOnlyList<Ball> GetAllBalls()
        {
            return _Ball_Repository.GetAllBalls();
        }


    }
}
