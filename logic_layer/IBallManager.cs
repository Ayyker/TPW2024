using data_layer;

namespace logic_layer
{
    public interface IBallManager
    {
        int Width { get; }
        int Height { get; }
        void GenerateBalls(int amount);
        void ClearAllBalls();
        IReadOnlyList<Ball> GetAllBalls();
    }
}
