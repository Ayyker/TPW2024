using System;
using System.Collections.ObjectModel;

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
