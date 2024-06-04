using data_layer;

namespace logic_layer {

    public class BallManager : IBallManager {
        private int _Width;
        private int _Height;
        private IBallRepository _Ball_Repository;
        private Random random = new Random();
        private Logger logger;
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


        public BallManager(int width, int height, IBallRepository ballRepository) {

            _Width = width;
            _Height = height;
            _Ball_Repository = ballRepository;
            List<Ball> balls = _Ball_Repository.GetAllBalls();
            logger = new Logger(balls);
        }
        public int Width {
            get => _Width;
        }

        public int Height {
            get => _Height;
        }

        public void GenerateBalls(int amount) {
            // hard coded value of ball radius and weight
            double weight = random.NextDouble() *2;
            //ballColors.Shuffle();
            int colorIndex = 0;
            for (int i = 0; i < amount; i++) {
                double radius = random.NextDouble() * 30 + 40;
                if ((i % 15) + 1 == 8) {
                    _Ball_Repository.AddBall(
                        new Ball(
                            radius,
                            radius * 0.2,
                            random.NextDouble() * (_Width - radius),
                            random.NextDouble() * (_Height - radius),
                            random.NextDouble() * 4,
                            random.NextDouble() * 4,
                            i,
                            "#010001"
                            )
                        );
                }
                else {
                    _Ball_Repository.AddBall(
                        new Ball(
                            radius,
                            radius * 0.2 ,
                            random.NextDouble() * (_Width - radius),
                            random.NextDouble() * (_Height - radius),
                            random.NextDouble() * 4,
                            random.NextDouble() * 4,
                            i,
                            ballColors[colorIndex]
                            )
                        );
                }
                colorIndex++;
                if (colorIndex > 14) {
                    colorIndex = 0;
                }
            }
        }

        public void ClearAllBalls() {
            _Ball_Repository.ClearAllBalls();
            logger.Stop();
        }
        public IReadOnlyList<Ball> GetAllBalls() {
            return _Ball_Repository.GetAllBalls();
        }


    }
}
