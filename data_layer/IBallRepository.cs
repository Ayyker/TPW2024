namespace data_layer {
    public interface IBallRepository {
        void AddBall(Ball ball);
        List<Ball> GetAllBalls();
        void ClearAllBalls();

    }
}
