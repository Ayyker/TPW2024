using data_layer;
using logic_layer;

namespace tests {
    public class BallManagerTests {
        BallManager _manager;

        [SetUp]
        public void Setup() {
           _manager = new BallManager(1000, 600, new BallRepository());
        }

        [Test]
        public void CreateBallManagerTest() {
            Assert.That(_manager.Width, Is.EqualTo(1000));
            Assert.That(_manager.Height, Is.EqualTo(600));
        }

        [Test]
        public void GenerateBallsTest() {
            _manager.GenerateBalls(5);

            Assert.That(_manager.GetAllBalls().Count, Is.EqualTo(5));
            var balls = _manager.GetAllBalls();

            foreach ( var ball in balls ) {
                Assert.IsTrue(ball.X_position >= 0 && ball.X_position + ball.Radius <= _manager.Width, "Ball should be within horizontal bounds.");
                Assert.IsTrue(ball.Y_position >= 0 && ball.Y_position + ball.Radius <= _manager.Height, "Ball should be within vertical bounds.");
            }
        }

        [Test]
        public void UpdateBallsTest() {
            _manager.GenerateBalls(10);
            var initialPositions = _manager.GetAllBalls().Select(ball => new { ball.X_position, ball.Y_position }).ToList();

            _manager.UpdateBalls();
            var updatedPositions = _manager.GetAllBalls().Select(ball => new { ball.X_position, ball.Y_position }).ToList();

            for (int i = 0; i < 10; i++) {
                var initial = initialPositions[i];
                var updated = updatedPositions[i];
                bool positionChanged = initial.X_position != updated.X_position || initial.Y_position != updated.Y_position;
                Assert.IsTrue(positionChanged, $"Ball {i} should have moved from its initial position.");
            }
        }

        [Test]
        public void ClearAllBallsTest() {
            Assert.IsEmpty(_manager.GetAllBalls());

            _manager.GenerateBalls(10);

            Assert.That(_manager.GetAllBalls().Count, Is.EqualTo(10));

            _manager.ClearAllBalls();

            Assert.That(_manager.GetAllBalls().Count, Is.EqualTo(0));
        }
    }
}
