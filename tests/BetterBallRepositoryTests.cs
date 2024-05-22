using data_layer;
using presentation_layer.Models;
using presentation_layer.ViewModels;

namespace tests {
    [TestFixture]
    public class BetterBallRepositoryTests {
        private BetterBallRepository _repository;
        private BetterBall _ball;

        [SetUp]
        public void Setup() {
            _repository = new BetterBallRepository();
            var ball = new Ball(45, 5, 50, 50, 5, 0, 1, "red");
            _ball = new BetterBall(ball, 1000, 600, _repository);
        }

        [Test]
        public void AddBallTest() {
            Assert.IsEmpty(_repository.GetAllBalls());
            _repository.AddBall(_ball);
            Assert.IsNotEmpty(_repository.GetAllBalls());
            Assert.Contains(_ball, _repository.GetAllBalls());
        }

        [Test]
        public void GetAllBallsTest() {
            _repository.AddBall(_ball);
            var balls = _repository.GetAllBalls();
            Assert.That(balls.Count, Is.EqualTo(1));
            Assert.That(balls, Contains.Item(_ball));
        }

        [Test]
        public void ClearAllBallsTest() {
            _repository.AddBall(_ball);
            Assert.That(_repository.GetAllBalls().Count, Is.EqualTo(1));
            _repository.ClearAllBalls();
            Assert.IsEmpty(_repository.GetAllBalls());
        }
    }
}