using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_layer;
using logic_layer;

namespace presentation_layer.Models
{
    public class SimulationUI : ISimulationObserver
    {
        private SimulationManager simulationManager;
        public SimulationUI(SimulationManager manager)
        {
            simulationManager = manager;
            simulationManager.RegisterObserver(this);
        }

        public void Update()
        {
            // odswieza GUI w odpowiedzi na zmiany w symulacji
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            // logika odswiezania interfejsu 
        }

        // zapewnia mozliwosc odrejestrowania obserwatora
        public void Detach()
        {
            simulationManager.RemoveObserver(this);
        }

        public void InitializeSimulation(int numberOfBalls, int width, int height)
        {
            simulationManager.InitializeSimulation(numberOfBalls, width, height);
    
        }

        public void UpdateSimulation()
        {
            simulationManager.UpdateSimulation();
        }

        public void AddBall(Ball ball)
        {
            simulationManager.AddBall(ball);
    
        }

        public void RemoveBall(Ball ball)
        {
            simulationManager.RemoveBall(ball);
  
        }
    }
}
