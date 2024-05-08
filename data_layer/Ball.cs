
using System.Drawing;

namespace data_layer
{
    public class Ball : IBall
    {
        private int _ID;
        private double _X_position;
        private double _Y_position;
        private double _X_velocity;
        private double _Y_velocity;
        private double _Radius;
        private string _Color;
        private int _Ball_Number;

        public Ball(double radius, double x, double y, double x_velocity, double y_velocity, int id, string color)
        {
            Radius = radius;
            X_position = x;
            Y_position = y;
            X_velocity = x_velocity;
            Y_velocity = y_velocity;
            ID = id;
            Color = color;
            Ball_Number = (id % 15) + 1;
        }
        public double Radius
        {
            get => _Radius;
            set => _Radius = value;
        }

        public double X_position
        {
            get => _X_position;
            set => _X_position = value;
        }
        public double Y_position
        {
            get => _Y_position;
            set => _Y_position = value;
        }
        public double X_velocity
        {
            get => _X_velocity;
            set => _X_velocity = value;
        }
        public double Y_velocity
        {
            get => _Y_velocity;
            set => _Y_velocity = value;
        }
        public int ID
        {
            get => _ID;
            set => _ID = value;
        }
        public string Color 
        {
            get => _Color;
            set => _Color = value;
        }
        public int Ball_Number 
        {
            get => _Ball_Number;
            set => _Ball_Number = value;
        }

        public override string ToString()
        {
            return $"Ball at ({X_position}, {Y_position}) with velocity ({X_velocity}, {Y_velocity}) and radius {Radius}.";
        }
    }
}
