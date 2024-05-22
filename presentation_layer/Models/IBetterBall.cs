using data_layer;
using presentation_layer.ViewModels;
using System.Threading.Tasks;

namespace presentation_layer.Models {
    public interface IBetterBall {
        public Ball Ball { get; set; }
        public void UpdateBall();
        public bool IsColliding(BetterBall otherBall);
        public Task ResolveCollision(BetterBall otherBall);
        public void Stop() { }
    }
}
