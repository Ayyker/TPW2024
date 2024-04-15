using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_layer
{
    public interface IBallRepository
    {
        void AddBall(Ball ball);
        IReadOnlyList<Ball> GetBalls();
        void UpdateBalls();
        void RemoveBall(Ball ball);
        void InitializeBalls(int numberOfBalls, int width, int height);
    }
}
