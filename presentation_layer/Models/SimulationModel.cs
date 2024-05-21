using data_layer;
using logic_layer;
using presentation_layer.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace presentation_layer.Models {
    public class SimulationModel : ISimulationModel {

        private IBallManager _Ball_Manager;
        private int _Width;
        private int _Height;

        public SimulationModel(int width, int height) {
            _Width = width;
            _Height = height;
            _Ball_Manager = new BallManager(_Width, _Height, new BallRepository());
        }

        public void GenerateBalls(int amount) {
            _Ball_Manager.GenerateBalls(amount);
        }

        public void ClearAllBalls() {
            _Ball_Manager.ClearAllBalls();
        }
        public ObservableCollection<Ball> GetBalls() {
            var balls = _Ball_Manager.GetAllBalls();
            return new ObservableCollection<Ball>(balls);
        }

        public void UpdateBalls() {
            _Ball_Manager.UpdateBalls();
        }

        public IBallManager BallManager {
            get => _Ball_Manager;
            set => _Ball_Manager = value;
        }
    }
}
