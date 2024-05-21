using data_layer;
using logic_layer;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace presentation_layer.Models {
    public interface ISimulationModel {
        public void GenerateBalls(int amount);
        public void ClearAllBalls();
        public ObservableCollection<Ball> GetBalls();
        public IBallManager BallManager { get; set; }
        public void UpdateBalls();


    }
}
