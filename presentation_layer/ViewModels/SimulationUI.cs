using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using data_layer;
using logic_layer;

namespace presentation_layer.ViewModels
{
    public class SimulationUI : ISimulationObserver
    {
        public SimulationUI(SimulationManager manager)
        {
            this.simulationManager = manager;
            simulationManager.RegisterObserver(this);
        }

        public void Update()
        {
            // Odśwież GUI w odpowiedzi na zmiany w symulacji
            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            // Logika odświeżania interfejsu użytkownika
        }

        // Zapewnij możliwość odrejestrowania obserwatora, gdy GUI jest zamykane lub nie jest już potrzebne
        public void Detach()
        {
            simulationManager.RemoveObserver(this);
        }
    }
}
