using data_layer;
using System;
using System.Windows.Threading;

namespace presentation_layer.ViewModels {
    public class BallWithTimer {
        public Ball Ball { get; private set; }

        public double X_position => Ball.X_position;
        public double Y_position => Ball.Y_position;
        public double Radius => Ball.Radius;
        public string Color => Ball.Color;
        public int Ball_Number => Ball.Ball_Number;
        private DispatcherTimer _timer;

        private int _Width;
        private int _Height;
        public BallWithTimer(Ball ball, int width, int height) {
            Ball = ball;
            _Width = width;
            _Height = height;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(100); // Możesz dostosować interwał zależnie od potrzeb symulacji
            _timer.Tick += (sender, args) => UpdateBall();
            _timer.Start();
        }

        private void UpdateBall() {
            double new_x_position = Ball.X_position + Ball.X_velocity;
            double new_y_position = Ball.Y_position + Ball.Y_velocity;

            if (new_x_position <= 0) {
                Ball.X_position = 0;
                Ball.X_velocity *= -1.0;
            }
            else if (new_x_position + Ball.Radius >= _Width) {
                Ball.X_position = _Width - Ball.Radius;
                Ball.X_velocity *= -1.0;
            }
            else {
                Ball.X_position = new_x_position;
            }

            if (new_y_position <= 0) {
                Ball.Y_position = 0;
                Ball.Y_velocity *= -1.0;
            }
            else if (new_y_position + Ball.Radius >= _Height) {
                Ball.Y_position = _Height - Ball.Radius;
                Ball.Y_velocity *= -1.0;
            }
            else {
                Ball.Y_position = new_y_position;
            }
        }

        public void Stop() {
            _timer.Stop();
        }
    }
}
