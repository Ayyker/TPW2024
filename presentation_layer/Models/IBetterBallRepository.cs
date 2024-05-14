using System.Collections.ObjectModel;

namespace presentation_layer.Models {
    public interface IBetterBallRepository {
        void AddBall(IBetterBall ball);
        ObservableCollection<IBetterBall> GetAllBalls();
        void ClearAllBalls();

    }
}
