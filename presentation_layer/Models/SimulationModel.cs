using data_layer;
using logic_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation_layer.Models
{
    public class SimulationModel
    {
        private IBallRepository _ballRepository;
        private IBallManager _ballManager;

        public SimulationModel(int width, int height)
        {
            _ballRepository = new BallRepository();
            _ballManager = new BallManager(_ballRepository, width, height); 
        }

        public void GenerateBalls(int amount)
        {
            _ballManager.GenerateBalls(amount);
        }

        public void UpdateBalls() 
        {
            _ballManager.UpdateBalls();
        }
        public IReadOnlyList<Ball> GetBalls()
        {
            return _ballManager.GetBalls();
        }
        public void ClearAllBalls()
        {
            _ballManager.ClearAllBalls();
        }
    }
}
