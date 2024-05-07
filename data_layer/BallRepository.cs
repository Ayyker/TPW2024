
namespace data_layer
{
    public class BallRepository : IBallRepository

    {
        private List<Ball> _Balls = new List<Ball>();

        public void AddBall(Ball ball)
        {
            if (ball != null)
            {
                _Balls.Add(ball);
            }
            else
            {
                throw new ArgumentNullException("ball", "Ball cannot be null");
            }
        }
        public List<Ball> GetAllBalls()
        {
            return _Balls;
        }

        public void ClearAllBalls()
        {
            _Balls.Clear();
        }

    }
}
