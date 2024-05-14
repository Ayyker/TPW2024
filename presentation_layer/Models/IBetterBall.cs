using data_layer;

namespace presentation_layer.Models {
    public interface IBetterBall {
        public Ball Ball { get; set; }
        private void UpdateBall() { }
        public void Stop() { }
    }
}
