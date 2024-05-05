using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_layer
{
    public class Ball: IBall
    {
        private int _ID;
        private double _X_position;
        private double _Y_position;
        private double _Velocity_X;
        private double _Velocity_Y;
        private double _Radius;

        public Ball(int id, double x, double y, double velocityX, double velocityY, double radius)
        {
            ID = id;
            X_position = x;
            Y_position = y;
            Velocity_X = velocityX;
            Velocity_Y = velocityY;
            Radius = radius;
        }

        public int ID
        {
            get => _ID; 
            set => _ID = value;
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

        public double Velocity_X
        {
            get => _Velocity_X;
            set => _Velocity_X = value;
        }
        public double Velocity_Y
        {
            get => _Velocity_Y;
            set => _Velocity_Y = value;
        }
        public double Radius
        {
            get => _Radius;
            set => _Radius = value;
        }





        public override string ToString()
        {
            return $"Ball at ({X_position}, {Y_position}) with velocity ({Velocity_X}, {Velocity_Y}) and radius {Radius}.";
        }
    }
}
