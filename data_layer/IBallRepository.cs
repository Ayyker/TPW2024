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
        List<Ball> GetAllBalls();
        void ClearAllBalls();

    }
}
