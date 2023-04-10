using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic;

namespace LogicTests
{
    [TestClass]
    public class BallTests
    {
        [TestMethod]
        public void TestBallConstructor()
        {
            // Arrange
            int x = 10;
            int y = 20;
            int size = 30;
            BallAPI ball = new BallAPI(x, y, size);

            // Act
            int actualX = ball.GetX();
            int actualY = ball.GetY();
            int actualSize = ball.GetSize();

            // Assert
            Assert.AreEqual(x, actualX);
            Assert.AreEqual(y, actualY);
            Assert.AreEqual(size, actualSize);
        }

        [TestMethod]
        public void TestBallProperties()
        {
            // Arrange
            BallAPI ball = new BallAPI(0, 0, 0);

            // Act
            ball.SetX(10);
            ball.SetY(20);

            // Assert
            Assert.AreEqual(10, ball.GetX());
            Assert.AreEqual(20, ball.GetY());
            Assert.AreEqual(0, ball.GetSize());
        }
    }

}

