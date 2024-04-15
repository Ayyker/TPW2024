using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logic_layer;


namespace presentation_layer.Models
{
    public class BallModel
    {
        public ObservableCollection<poolBall> Balls { get; private set; }
        
        public void BaallModel()
        {
            Balls = new ObservableCollection<poolBall>();
        }

        public void AddBall(poolBall ball)
        {
            Balls.Add(ball);
        }

    }
}
