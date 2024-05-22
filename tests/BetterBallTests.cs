using data_layer;
using presentation_layer.Models;
using presentation_layer.ViewModels;

namespace tests {
    [TestFixture]
    public class BetterBallTests {
        private BetterBall _ball1;
        private BetterBall _ball2;
        private BetterBallRepository _repository;

        [SetUp]
        public void Setup() {
            var ball1 = new Ball(45, 5, 50, 50, 5, 0, 1, "red");
            var ball2 = new Ball(45, 5, 100, 50, -5, 0, 2, "blue");
            _repository = new BetterBallRepository();
            _ball1 = new BetterBall(ball1, 1000, 600, _repository);
            _ball2 = new BetterBall(ball2, 1000, 600, _repository);
            _repository.AddBall(_ball1);
            _repository.AddBall(_ball2);
        }

        [Test]
        public void TestBallBouncesOffWalls() {
            var ball = new Ball(45, 5, 10, 10, -5, -5, 1, "red");
            var betterBall = new BetterBall(ball, 1000, 600, _repository);
            _repository.AddBall(betterBall);

            // Aktualizacja pozycji kul
            for (int i = 0; i < 10; i++) {
                betterBall.UpdateBall();
            }

            Assert.That(betterBall.X_position >= 0 && betterBall.X_position + betterBall.Radius <= 1000, Is.True);
            Assert.That(betterBall.Y_position >= 0 && betterBall.Y_position + betterBall.Radius <= 600, Is.True);
        }

        [Test]
        public void TestBallAtBoundary() {
            var ball = new Ball(45, 5, 955, 555, 5, 5, 1, "blue");
            var betterBall = new BetterBall(ball, 1000, 600, _repository);
            _repository.AddBall(betterBall);

            // Aktualizacja pozycji kul
            betterBall.UpdateBall();

            Assert.That(betterBall.X_velocity < 0, Is.True);
            Assert.That(betterBall.Y_velocity < 0, Is.True);
        }
        [Test]
        public void TestBallWithHighVelocity() {
            var ball = new Ball(45, 5, 500, 300, 50, 50, 1, "green");
            var betterBall = new BetterBall(ball, 1000, 600, _repository);
            _repository.AddBall(betterBall);

            // Aktualizacja pozycji kul
            for (int i = 0; i < 10; i++) {
                betterBall.UpdateBall();
            }

            Assert.That(betterBall.X_position >= 0 && betterBall.X_position + betterBall.Radius <= 1000, Is.True);
            Assert.That(betterBall.Y_position >= 0 && betterBall.Y_position + betterBall.Radius <= 600, Is.True);
        }

        [Test]
        public void TestBallWithZeroVelocity() {
            var ball = new Ball(45, 5, 500, 300, 0, 0, 1, "yellow");
            var betterBall = new BetterBall(ball, 1000, 600, _repository);
            _repository.AddBall(betterBall);

            // Aktualizacja pozycji kul
            betterBall.UpdateBall();

            Assert.That(betterBall.X_position, Is.EqualTo(500));
            Assert.That(betterBall.Y_position, Is.EqualTo(300));
        }
        [Test]
        public void TestPerformanceWithManyBalls() {
            var model = new SimulationModel(1000, 600);
            model.GenerateBalls(1000);
            var startTime = DateTime.Now;

            model.GetBalls().ToList().ForEach(ball => ((BetterBall)ball).UpdateBall());

            var duration = DateTime.Now - startTime;
            Assert.Less(duration.TotalSeconds, 1, "Updating positions of 1000 balls should take less than a second.");
        }

        [Test]
        public void TestBallWithExtremeWeight() {
            var ball = new Ball(45, 1000, 500, 300, 10, 10, 1, "black");
            var betterBall = new BetterBall(ball, 1000, 600, _repository);
            _repository.AddBall(betterBall);

            betterBall.UpdateBall();

            Assert.That(betterBall.X_velocity, Is.EqualTo(10));
            Assert.That(betterBall.Y_velocity, Is.EqualTo(10));
        }
    }
}