using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace presentation_layer.Models {
    public class BetterBallRepository : IBetterBallRepository {
        private ObservableCollection<IBetterBall> _Balls;
        private readonly Dispatcher _dispatcher;

        public BetterBallRepository() {
            _Balls = new ObservableCollection<IBetterBall>();
            _dispatcher = Dispatcher.CurrentDispatcher;
        }

        public void AddBall(IBetterBall ball) {
            if (ball != null) {
                if (_dispatcher.CheckAccess()) {
                    _Balls.Add(ball);
                } else {
                    _dispatcher.Invoke(() => _Balls.Add(ball));
                }
            } else {
                throw new ArgumentNullException(nameof(ball));
            }
        }

        public ObservableCollection<IBetterBall> GetAllBalls() {
            return _Balls;
        }

        public void ClearAllBalls() {
            if (_dispatcher.CheckAccess()) {
                _Balls.Clear();
            } else {
                _dispatcher.Invoke(() => _Balls.Clear());
            }
        }
    }
}