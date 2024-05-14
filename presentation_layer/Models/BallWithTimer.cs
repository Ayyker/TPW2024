using data_layer;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace presentation_layer.ViewModels {
    public class BallWithTimer : INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Ball Ball { get; private set; }

        public double X_position {
            get => Ball.X_position;
            set {
                if (Ball.X_position != value) {
                    Ball.X_position = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Y_position {
            get => Ball.Y_position;
            set {
                if (Ball.Y_position != value) {
                    Ball.Y_position = value;
                    OnPropertyChanged();
                }
            }
        }
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
            _timer.Interval = TimeSpan.FromMilliseconds(1); 
            _timer.Tick += (sender, args) => UpdateBall();
            _timer.Start();
        }

        private void UpdateBall() {
            double new_x_position = Ball.X_position + Ball.X_velocity;
            double new_y_position = Ball.Y_position + Ball.Y_velocity;

            if (new_x_position <= 0) {
                X_position = 0;
                Ball.X_velocity *= -1.0;
            }
            else if (new_x_position + Ball.Radius >= _Width) {
                X_position = _Width - Ball.Radius;
                Ball.X_velocity *= -1.0;
            }
            else {
                X_position = new_x_position;
            }

            if (new_y_position <= 0) {
                Y_position = 0;
                Ball.Y_velocity *= -1.0;
            }
            else if (new_y_position + Ball.Radius >= _Height) {
                Y_position = _Height - Ball.Radius;
                Ball.Y_velocity *= -1.0;
            }
            else {
                Y_position = new_y_position;
            }
        }

        public void Stop() {
            _timer.Stop();
        }
    }
}
