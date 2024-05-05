using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data_layer
{
    public class Ball: i_Ball
    {
        private int _ID;
        private double _X_position;
        private double _Y_position;
        private double _

       
        public double X { get; set; }
        public double Y { get; set; }

        
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }

        
        public double Radius { get; set; }

        
        public Ball(double x, double y, double velocityX, double velocityY, double radius)
        {
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
            Radius = radius;
        }

        
        public void UpdatePosition()
        {
            X += VelocityX;
            Y += VelocityY;
        }

        
        public override string ToString()
        {
            return $"Ball at ({X}, {Y}) with velocity ({VelocityX}, {VelocityY}) and radius {Radius}.";
        }
    }
}
