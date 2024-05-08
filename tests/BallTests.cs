using data_layer;

namespace tests {
    public class BallTests {
        Ball _ball = new Ball(45, 20, 20, 20, 20, 1);

        [Test]
        public void GetterTest() {
            Assert.That(_ball.Radius, Is.EqualTo(45));
            Assert.That(_ball.X_position, Is.EqualTo(20));
            Assert.That(_ball.Y_position, Is.EqualTo(20));
            Assert.That(_ball.X_velocity, Is.EqualTo(20));
            Assert.That(_ball.Y_velocity, Is.EqualTo(20));
            Assert.That(_ball.ID, Is.EqualTo(1));
        }

        [Test]
        public void SetterTest() {
            _ball.Radius = 40;
            _ball.X_position = 10;
            _ball.Y_position = 10;
            _ball.X_velocity = 10;
            _ball.Y_velocity = 10;
            _ball.ID = 2;

            Assert.That(_ball.Radius, Is.EqualTo(40));
            Assert.That(_ball.X_position, Is.EqualTo(10));
            Assert.That(_ball.Y_position, Is.EqualTo(10));
            Assert.That(_ball.X_velocity, Is.EqualTo(10));
            Assert.That(_ball.Y_velocity, Is.EqualTo(10));
            Assert.That(_ball.ID, Is.EqualTo(2));
        }

        [Test]
        public void ToStringTest() {
            Ball _ball2 = new Ball(84, 2, 1, 3, 7, 1);
            Assert.That(_ball2.ToString(), Is.EqualTo("Ball at (2, 1) with velocity (3, 7) and radius 84."));
        }
    }
}
