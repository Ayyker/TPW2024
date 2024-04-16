using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_layer;

namespace logic_layer
{
    public class SimulationManager : ISimulationManager
    {
        private IBallRepository ballRepository;
        //tutaj przetrzymywani sa obserwatorzy
        private List<ISimulationObserver> observers;

        private Random random = new Random();
        public SimulationManager(IBallRepository ballRepository, ISimulationObserver observer = null)
        {
            this.ballRepository = ballRepository;
            this.observers = new List<ISimulationObserver>();
            if (observer != null)
            {
                observers.Add(observer);
            }
        }

        public void InitializeSimulation(int numberOfBalls, int width, int height)
        {
            for (int i = 0; i < numberOfBalls; i++)
            {
                double x = random.NextDouble() * width;
                double y = random.NextDouble() * height;
                double velocityX = random.NextDouble() * 2 - 1; //do zmiany
                double velocityY = random.NextDouble() * 2 - 1; //do zmiany
                double radius = 5; //do zmiany

                Ball newBall = new Ball(x, y, velocityX, velocityY, radius);
                AddBall(newBall);
            }
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
