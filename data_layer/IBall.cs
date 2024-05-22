namespace data_layer {
    public interface IBall {
        public double Radius { get; set; }
        public double Weight { get; set; }
        public double X_position { get; set; }
        public double Y_position { get; set; }
        public double X_velocity { get; set; }
        public double Y_velocity { get; set; }
        public int ID { get; set; }
        public string Color { get; set; }
        public int Ball_Number { get; set; }

    }
}
