using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace presentation_layer.Models {
    public interface ISimulationModel {
        public Task GenerateBalls(int amount);
        public Task ClearAllBalls();
        public ObservableCollection<IBetterBall> GetBalls();
    }
}
