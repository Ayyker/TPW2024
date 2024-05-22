using data_layer;
using presentation_layer.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace presentation_layer.ViewModels {
    public class BetterBall : IBetterBall, INotifyPropertyChanged {
        private int _Width;
        private int _Height;
        private readonly IBetterBallRepository _repository;
        private readonly object _lock = new object();
        private CancellationTokenSource _cancellationTokenSource;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Ball Ball { get; set; }

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

        public BetterBall(Ball ball, int width, int height, IBetterBallRepository repository) {
            Ball = ball;
            _Width = width;
            _Height = height;
            _repository = repository;

            _cancellationTokenSource = new CancellationTokenSource();
            StartSimulation();
        }

        private void StartSimulation() {
            var token = _cancellationTokenSource.Token;
            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested) {
                    UpdateBall();
                    await Task.Delay(10, token);
                }
            }, token);
        }

        public void UpdateBall() {
            lock (_lock) {
                // Aktualizacja pozycji kuli na podstawie jej prędkości
                double new_x_position = X_position + X_velocity;
                double new_y_position = Y_position + Y_velocity;

                // Odbicie od ścian
                if (new_x_position <= 0 || new_x_position + Radius >= _Width) {
                    X_velocity *= -1.0;
                    new_x_position = X_position + X_velocity; // Aktualizacja pozycji po odbiciu
                    Console.WriteLine($"Ball {Ball_Number} bounced horizontally. New velocity: ({X_velocity}, {Y_velocity}).");
                }
                if (new_y_position <= 0 || new_y_position + Radius >= _Height) {
                    Y_velocity *= -1.0;
                    new_y_position = Y_position + Y_velocity; // Aktualizacja pozycji po odbiciu
                    Console.WriteLine($"Ball {Ball_Number} bounced vertically. New velocity: ({X_velocity}, {Y_velocity}).");
                }

                X_position = new_x_position;
                Y_position = new_y_position;

                // Sprawdzanie kolizji z innymi kulami
                foreach (var otherBall in _repository.GetAllBalls().OfType<BetterBall>()) {
                    if (otherBall != this && IsColliding(otherBall)) {
                        Console.WriteLine($"Ball {Ball_Number} collided with Ball {otherBall.Ball_Number}.");
                        ResolveCollision(otherBall);
                    }
                }
            }
        }

        public bool IsColliding(BetterBall otherBall) {
            double dx = (otherBall.X_position + otherBall.Radius / 2) - (this.X_position + this.Radius / 2);
            double dy = (otherBall.Y_position + otherBall.Radius / 2) - (this.Y_position + this.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);
            return distance < (this.Radius / 2 + otherBall.Radius / 2);
        }

        public void ResolveCollision(BetterBall otherBall) {
            Console.WriteLine($"Resolving collision between Ball {Ball_Number} and Ball {otherBall.Ball_Number}.");

            // Wejściowe prędkości
            double vx1 = this.X_velocity;
            double vy1 = this.Y_velocity;
            double vx2 = otherBall.X_velocity;
            double vy2 = otherBall.Y_velocity;

            // Masa
            double m1 = this.Ball.Weight;
            double m2 = otherBall.Ball.Weight;

            // Różnica pozycji
            double dx = (otherBall.X_position + otherBall.Radius / 2) - (this.X_position + this.Radius / 2);
            double dy = (otherBall.Y_position + otherBall.Radius / 2) - (this.Y_position + this.Radius / 2);

            // Odległość
            double distance = Math.Sqrt(dx * dx + dy * dy);

            if (distance == 0) {
                // Zapobiegaj dzieleniu przez zero
                distance = this.Radius + otherBall.Radius;
                dx = distance;
                dy = 0;
            }

            // Normalizacja wektora
            double nx = dx / distance;
            double ny = dy / distance;

            // Składowe prędkości wzdłuż normalnej
            double p = 2 * (vx1 * nx + vy1 * ny - vx2 * nx - vy2 * ny) / (m1 + m2);

            double newXSpeedForBall = vx1 - p * m2 * nx;
            double newYSpeedForBall = vy1 - p * m2 * ny;
            double newXSpeedForBall2 = vx2 + p * m1 * nx;
            double newYSpeedForBall2 = vy2 + p * m1 * ny;

            lock (_lock) {
                // Aktualizacja prędkości po kolizji
                this.X_velocity = newXSpeedForBall;
                this.Y_velocity = newYSpeedForBall;
                otherBall.X_velocity = newXSpeedForBall2;
                otherBall.Y_velocity = newYSpeedForBall2;
            }

            // Sprawdzenie, czy kule zachodzą na siebie
            double overlap = (this.Radius / 2 + otherBall.Radius / 2) - distance;

            if (overlap > 0) {
                // Przesunięcie kul tak, aby nie zachodziły na siebie
                double correction = overlap / 2;

                double correctionX = correction * nx;
                double correctionY = correction * ny;

                // Nowe pozycje kul
                double thisNewX = this.X_position - correctionX;
                double thisNewY = this.Y_position - correctionY;
                double otherNewX = otherBall.X_position + correctionX;
                double otherNewY = otherBall.Y_position + correctionY;

                // Upewnienie się, że kule pozostają w granicach planszy
                if (thisNewX - this.Radius / 2 >= 0 && thisNewX + this.Radius / 2 <= _Width) {
                    this.X_position = thisNewX;
                }
                if (thisNewY - this.Radius / 2 >= 0 && thisNewY + this.Radius / 2 <= _Height) {
                    this.Y_position = thisNewY;
                }
                if (otherNewX - otherBall.Radius / 2 >= 0 && otherNewX + otherBall.Radius / 2 <= _Width) {
                    otherBall.X_position = otherNewX;
                }
                if (otherNewY - otherBall.Radius / 2 >= 0 && otherNewY + otherBall.Radius / 2 <= _Height) {
                    otherBall.Y_position = otherNewY;
                }
            }

            // Dodanie logowania po rozwiązaniu kolizji
            Console.WriteLine($"Collision resolved. Ball {Ball_Number} new velocity: ({this.X_velocity}, {this.Y_velocity}). Ball {otherBall.Ball_Number} new velocity: ({otherBall.X_velocity}, {otherBall.Y_velocity}).");
        }

        public void Stop() {
            _cancellationTokenSource.Cancel();
        }
    }
}
