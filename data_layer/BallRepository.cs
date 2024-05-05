using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_layer
{
    public class BallRepository : IBallRepository

    {   //target-typed expression
        private List<Ball> _Balls = new(); 
 
        public void AddBall(Ball ball)
        {
            if (ball != null)
            {
                _Balls.Add(ball);
            }
            else
            {
                //throw new ArgumentNullException("ball", "Ball cannot be null");
                return;
            }
        }

        public IReadOnlyList<Ball> GetBalls()
        {
            return _Balls.ToArray();
        }

        public void ClearAllBalls()
        {
            _Balls.Clear();
        }

    }
}
