using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic_layer
{
    public class SimulationManager
    {
        //to trzeba zmienic
        double maxWidth = 2;
        double maxHeight = 2;
        double maxVelocity = 2;
        double defaultRadius = 2;

        private List<PoolBall> balls = new List<PoolBall>();
        private Random random = new Random();

        public void AddBall()
        {
            double x = random.NextDouble() * maxWidth;
            double y = random.NextDouble() * maxHeight;
            double velocityX = random.NextDouble() * maxVelocity;
            double velocityY = random.NextDouble() * maxVelocity;
            double radius = defaultRadius;

            var ball = new PoolBall(x, y, velocityX, velocityY, radius);
            balls.Add(ball);
        }
    }
}
