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
        public void TestCollision() {
            _ball1.UpdateBall();
            _ball2.UpdateBall();
            Assert.That(_ball1.X_velocity, Is.Not.EqualTo(5));
            Assert.That(_ball2.X_velocity, Is.Not.EqualTo(-5));
        }

        [Test]
        public void TestNoOverlapAfterCollision() {
            _ball1.UpdateBall();
            _ball2.UpdateBall();
            double dx = (_ball2.X_position + _ball2.Radius / 2) - (_ball1.X_position + _ball1.Radius / 2);
            double dy = (_ball2.Y_position + _ball2.Radius / 2) - (_ball1.Y_position + _ball1.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);
            Assert.That(distance, Is.GreaterThanOrEqualTo(_ball1.Radius / 2 + _ball2.Radius / 2));
        }

        [Test]
        public void TestNoOverlapAfterInitialization() {
            // Tworzymy dwie kule blisko siebie
            var ball3 = new Ball(45, 5, 100, 100, 0, 0, 3, "green");
            var ball4 = new Ball(45, 5, 140, 100, 0, 0, 4, "yellow");
            var betterBall3 = new BetterBall(ball3, 1000, 600, _repository);
            var betterBall4 = new BetterBall(ball4, 1000, 600, _repository);
            _repository.AddBall(betterBall3);
            _repository.AddBall(betterBall4);

            // Aktualizujemy pozycje kul
            betterBall3.UpdateBall();
            betterBall4.UpdateBall();

            double dx = (betterBall4.X_position + betterBall4.Radius / 2) - (betterBall3.X_position + betterBall3.Radius / 2);
            double dy = (betterBall4.Y_position + betterBall4.Radius / 2) - (betterBall3.Y_position + betterBall3.Radius / 2);
            double distance = Math.Sqrt(dx * dx + dy * dy);

            Assert.That(distance, Is.GreaterThanOrEqualTo(betterBall3.Radius / 2 + betterBall4.Radius / 2));
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