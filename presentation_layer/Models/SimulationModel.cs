using data_layer;
using logic_layer;
using System.Collections.Generic;

namespace presentation_layer.Models
{
    public class SimulationModel
    {
        private IBallRepository _Ball_Repository;
        private IBallManager _Ball_Manager;

        public SimulationModel(int width, int height)
        {
            _Ball_Repository = new BallRepository();
            _Ball_Manager = new BallManager(width, height, _Ball_Repository);
        }

        public void GenerateBalls(int amount)
        {
            _Ball_Manager.GenerateBalls(amount);
        }

        public void UpdateBalls()
        {
            _Ball_Manager.UpdateBalls();
        }
        public void ClearAllBalls()
        {
            _Ball_Manager.ClearAllBalls();
        }
        public IReadOnlyList<Ball> GetBalls()
        {
            return _Ball_Manager.GetAllBalls();
        }

    }
}
