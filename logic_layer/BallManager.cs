using data_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic_layer
{
    public class BallManager : IBallManager
    {
        private IBallRepository _ballRepository;
        private int _width;
        private int _height;

        public BallManager(IBallRepository ballRepository, int width, int height)
        {
            _ballRepository = ballRepository;
            _width = width;
            _height = height;
        }
        public void GenerateBalls(int amount)
        {
            Random random = new Random();
            // hard value of ball radius
            double radius = 45.0;
            for (int i = 0; i < amount; i++)
            {
                _ballRepository.AddBall(
                    new Ball(
                        i,
                        random.NextDouble() * (_width - radius),
                        random.NextDouble() * (_height - radius),
                        random.NextDouble() * 2,
                        random.NextDouble() * 2,
                        radius
                        )
                    );
            }
        }
        public void UpdateBalls()
        {
            foreach(Ball ball in GetBalls())
            {
                double new_x_position = ball.X_position + ball.Velocity_X;
                double new_y_position = ball.Y_position + ball.Velocity_Y;

                //x position logic
                if ( new_x_position <= 0)
                {
                    ball.X_position = 0;
                    ball.Velocity_X *= -1;
                }
                else if ( new_x_position + ball.Radius >= _width)
                {
                    ball.X_position = _width - ball.Radius;
                    ball.Velocity_X *= -1;
                }
                else
                {
                    ball.X_position = new_x_position;
                }

                //y position logic
                if ( new_y_position <= 0)
                {
                    ball.Y_position = 0;
                    ball.Velocity_Y *= -1;
                }
                else if(new_y_position + ball.Radius >= _height)
                {
                    ball.Y_position= _height - ball.Radius;
                    ball.Velocity_Y *= -1;
                }
                else
                {
                    ball.Y_position = new_y_position;
                }
            }
        }









        public int Width
        {
            get => _width;
        }

        public int Height
        {
            get => _height;
        }

        public void ClearAllBalls()
        {
            _ballRepository.ClearAllBalls();
        }

 

        public IReadOnlyList<Ball> GetBalls()
        {
            return _ballRepository.GetBalls();
        }

  
    }
}
