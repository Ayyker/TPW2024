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
        private List<ISimulationObserver> observers = new List<ISimulationObserver>();
        public SimulationManager(IBallRepository ballRepository)
        {
            this.ballRepository = ballRepository;
        }

        public void InitializeSimulation(int numberOfBalls, int width, int height)
        {
            ballRepository.InitializeBalls(numberOfBalls, width, height);
            NotifyObservers();
        }

        public void UpdateSimulation()
        {
            ballRepository.UpdateBalls();
            NotifyObservers();
        }

        public void AddBall(Ball ball)
        {
            ballRepository.AddBall(ball);
            NotifyObservers();
        }

        public void RemoveBall(Ball ball)
        {
            ballRepository.RemoveBall(ball);
            NotifyObservers();
        }

        public void RegisterObserver(ISimulationObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }
        public void RemoveObserver(ISimulationObserver observer)
        {
            observers.Remove(observer);
        }

        protected void NotifyObservers()
        {
            foreach (ISimulationObserver observer in observers)
            {
                observer.Update();
            }
        }
    }
}
