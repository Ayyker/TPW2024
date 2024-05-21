using data_layer;
using System.Collections.ObjectModel;

namespace logic_layer {
    public interface IBallManager {
        int Width { get; }
        int Height { get; }
        void GenerateBalls(int amount);
        void ClearAllBalls();
        List<Ball> GetAllBalls();
        public void UpdateBalls();
        event NotifyDelegateBallManager.NotifyBallManager? OnChange;
    }
}
