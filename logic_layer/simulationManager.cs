using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic_layer
{
    public class simulationManager

    {
        //to trzeba zmienic
        double maxWidth = 1;
        double maxHeight = 1;

        public List<poolBall> balls = new List<poolBall>();
        private Random random = new Random();

        public bool AddBall()
        {
            double X = random.NextDouble() * maxWidth;
            double Y = random.NextDouble() * maxHeight;
            double VelocityX = random.NextDouble();
            double VelocityY = random.NextDouble();
            poolBall ball = new poolBall(X,Y,VelocityX,VelocityY);
            balls.Add(ball);
            return true;
        }

        public void UpdateSimulation(double time)
        {
            foreach (var ball in balls)
            {
                ball.UpdatePosition(time);
                
            }
        }
    }
}
