using data_layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation_layer.Models {
    internal class BetterBallRepository : IBetterBallRepository {
        private ObservableCollection<IBetterBall> _Balls = new ObservableCollection<IBetterBall>();

        public void AddBall(IBetterBall ball) {
            if (ball != null) {
                _Balls.Add(ball);
            }
            else {
                throw new ArgumentNullException("ball", "Ball cannot be null");
            }
        }
        public ObservableCollection<IBetterBall> GetAllBalls() {
            return _Balls;
        }

        public void ClearAllBalls() {
            _Balls.Clear();
        }
 
    }
}
