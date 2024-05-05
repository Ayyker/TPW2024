using data_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic_layer
{
    public interface IBallManager
    {
        void GenerateBalls(int amount);
        void UpdateBalls();
        int Width { get; }
        int Height { get; }
        IReadOnlyList<Ball> GetBalls();
        void ClearAllBalls();
    }
}
