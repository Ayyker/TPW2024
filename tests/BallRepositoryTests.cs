using data_layer;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace tests {
    public class BallRepositoryTests {
        BallRepository _repo;
        Ball _ball;
        Ball _ball2;
        Ball? _ball3;

        [SetUp]
        public void Setup() {
            _repo = new BallRepository();
            _ball = new Ball(45, 20, 20, 20, 20, 1, "black");
            _ball2 = new Ball(50, 20, 20, 20, 20, 2, "black");
            _ball3 = null;
        }

        [Test]
        public void AddTest() {
            Assert.IsEmpty(_repo.GetAllBalls());
            _repo.AddBall(_ball);
            _repo.AddBall(_ball2);
            Assert.IsNotEmpty(_repo.GetAllBalls());
            Assert.That(_repo.GetAllBalls(), Contains.Item(_ball));
            Assert.That(_repo.GetAllBalls(), Contains.Item(_ball2));
            var ex = Assert.Throws<ArgumentNullException>(() => _repo.AddBall(_ball3));
            Assert.That(ex.ParamName, Is.EqualTo("ball"), "Expected ArgumentNullException for parameter 'ball'");
        }

        [Test]
        public void GetAllBallsTest() {
            Assert.IsEmpty(_repo.GetAllBalls());
            _repo.AddBall(_ball);
            _repo.AddBall(_ball2);

            var result = _repo.GetAllBalls();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result, Contains.Item(_ball));
            Assert.That(result, Contains.Item(_ball2));
        }

        [Test]
        public void ClearAllBallsTest() {
            Assert.IsEmpty(_repo.GetAllBalls());

            _repo.AddBall(_ball);
            _repo.AddBall(_ball2);
            Assert.That(_repo.GetAllBalls().Count, Is.EqualTo(2));

            _repo.ClearAllBalls();

            Assert.That(_repo.GetAllBalls().Count, Is.EqualTo(0));
        }
    }
}
