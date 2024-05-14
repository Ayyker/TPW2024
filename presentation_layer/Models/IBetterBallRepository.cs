using data_layer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation_layer.Models {
    public interface IBetterBallRepository {
        void AddBall(IBetterBall ball);
        ObservableCollection<IBetterBall> GetAllBalls();
        void ClearAllBalls();

    }
}
