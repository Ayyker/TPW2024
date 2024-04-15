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
        private Random random = new Random();

        public void InitializeBalls(int numberOfBalls, int width, int height)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                double x = random.NextDouble() * width;
                double y = random.NextDouble() * height;
                double velocityX = random.NextDouble() * 2 - 1; //do zmiany
                double velocityY = random.NextDouble() * 2 - 1; //do zmiany
                double radius = 5; //do zmiany

                Ball newBall = new Ball(x,y, velocityX, velocityY, radius);
                AddBall(newBall);
            }
        }
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

        public void UpdateBalls()
        {
            foreach (Ball b in balls)
            {
                b.UpdatePosition();
            }
        }
    }
}
