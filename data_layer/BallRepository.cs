using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_layer
{
    public class BallRepository : IBallRepository
    {
        private List<Ball> balls = new List<Ball>();
        public void AddBall(Ball ball)
        {
            balls.Add(ball);
        }

        public IReadOnlyList<Ball> GetBalls()
        {
            return balls.ToArray();
        }

        public void RemoveBall(Ball ball)
        {
            balls.Remove(ball);
        }

        public void UpdateBalls(Ball ball)
        {
            foreach (Ball b in balls)
            {
                b.UpdatePosition();
            }
        }
    }
}
