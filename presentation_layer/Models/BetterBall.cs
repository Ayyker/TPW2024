using data_layer;
using presentation_layer.Models;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace presentation_layer.ViewModels {
    public class BetterBall : IBetterBall, INotifyPropertyChanged{

        private DispatcherTimer _timer;
        private int _Width;
        private int _Height;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Ball Ball { get;  set; }
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
        public double X_velocity {
            get => Ball.X_velocity;
            set => Ball.X_velocity = value;
        }
        public double Y_velocity {
            get => Ball.Y_velocity;
            set => Ball.Y_velocity = value;
        }
        public double Radius => Ball.Radius;
        public string Color => Ball.Color;
        public int Ball_Number => Ball.Ball_Number;

        public BetterBall(Ball ball, int width, int height) {
            Ball = ball;
            _Width = width;
            _Height = height;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1); 
            _timer.Tick += (sender, args) => UpdateBall();
            _timer.Start();
        }
        //tutaj przenieśliśmy naszą implementacje aktualizacji pozycji dla pojedynczej kuli
        private void UpdateBall() {
            double new_x_position = X_position + X_velocity;
            double new_y_position = Y_position + Y_velocity;

            if (new_x_position <= 0) {
                X_position = 0;
                X_velocity *= -1.0;
            }
            else if (new_x_position + Radius >= _Width) {
                X_position = _Width - Radius;
                Ball.X_velocity *= -1.0;
            }
            else {
                X_position = new_x_position;
            }

            if (new_y_position <= 0) {
                Y_position = 0;
                Ball.Y_velocity *= -1.0;
            }
            else if (new_y_position + Radius >= _Height) {
                Y_position = _Height - Radius;
                Y_velocity *= -1.0;
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
