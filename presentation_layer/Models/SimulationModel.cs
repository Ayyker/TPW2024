using data_layer;
using logic_layer;
using presentation_layer.ViewModels;
using System.Collections.ObjectModel;

namespace presentation_layer.Models {
    public class SimulationModel : ISimulationModel {

        private IBallManager _Ball_Manager;
        private IBetterBallRepository _BetterBallRepository;
        private int _Width;
        private int _Height;

        public SimulationModel(int width, int height) {
            _Width = width;
            _Height = height;
            _Ball_Manager = new BallManager(_Width, _Height, new BallRepository());
            _BetterBallRepository = new BetterBallRepository();
        }

        public void GenerateBalls(int amount) {
            _Ball_Manager.GenerateBalls(amount);
            foreach (Ball ball in _Ball_Manager.GetAllBalls()) {
                _BetterBallRepository.AddBall(new BetterBall(ball, _Width, _Height));
            }
        }

        public void ClearAllBalls() {
            foreach (BetterBall betterBall in _BetterBallRepository.GetAllBalls()) {
                betterBall.Stop();
            }
            _Ball_Manager.ClearAllBalls();
            _BetterBallRepository.ClearAllBalls();
        }
        public ObservableCollection<IBetterBall> GetBalls() {
            return _BetterBallRepository.GetAllBalls();
        }

    }
}
