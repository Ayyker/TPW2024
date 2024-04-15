using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_layer;

namespace logic_layer
{
    public class SimulationManager
    {
        private IBallRepository ballRepository;
        public SimulationManager(IBallRepository ballRepository)
        {
            this.ballRepository = ballRepository;
        }

        public void InitializeSimulation(int numberOfBalls, int width, int height)
        {
            ballRepository.InitializeBalls(numberOfBalls, width, height);
        }

        public void UpdateSimulation()
        {
            ballRepository.UpdateBalls();
        }

        public void AddBall(Ball ball)
        {
            ballRepository.AddBall(ball);
        }

        public void RemoveBall(Ball ball)
        {
            ballRepository.RemoveBall(ball);
        }
    }
}
