namespace logic_layer
{
    public class poolBall
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double VelocityX { get; set; }
        public double VelocityY { get; set; }
        
        public poolBall(double x, double y, double velocityX, double velocityY)
        {
            X = x;
            Y = y;
            VelocityX = velocityX;
            VelocityY = velocityY;
        }

        public void UpdatePosition(double time)
        {
            // updated position based on time and velocity
            X += VelocityX * time;
            Y += VelocityY * time;

            
        }

    }
}
