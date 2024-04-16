using data_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logic_layer
{
    public interface ISimulationManager
    {
        void InitializeSimulation(int numberOfBalls, int width, int height);
        void UpdateSimulation();
        void AddBall(Ball ball);
        void RemoveBall(Ball ball);

    }
}
