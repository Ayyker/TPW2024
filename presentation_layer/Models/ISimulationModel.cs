﻿using System.Collections.ObjectModel;

namespace presentation_layer.Models {
    public interface ISimulationModel {
        public void GenerateBalls(int amount);
        public void ClearAllBalls();
        public ObservableCollection<IBetterBall> GetBalls();
    }
}