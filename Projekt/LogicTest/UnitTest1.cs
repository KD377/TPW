using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class TimerApiTests
    {

        [TestMethod]
        public void TestInterval()
        {
            // Arrange
            TimerApi timer = TimerApi.CreateBallTimer();
            TimeSpan interval = TimeSpan.FromMilliseconds(500);

            // Act
            timer.Interval = interval;

            // Assert
            Assert.AreEqual(interval, timer.Interval);
        }
    }



    [TestClass]
    public class BallsAPITests
    {
        [TestMethod]
        public void TestCreateBalls()
        {
            // Arrange
            BallsAPI api = new BallsAPI(800, 600);
            int expectedCount = 5;

            // Act
            api.CreateBalls(expectedCount);

            // Assert
            Assert.AreEqual(expectedCount, api.GetBallsNumber());
        }

        [TestMethod]
        public void TestMoveBalls()
        {
            // Arrange
            BallsAPI api = new BallsAPI(800, 600);
            api.CreateBalls(1);
            int initialX = api.GetX(0);
            int initialY = api.GetY(0);

            // Act
            api.Start();
            System.Threading.Thread.Sleep(1000);
            api.Stop();

            // Assert
            int newX = api.GetX(0);
            int newY = api.GetY(0);
            Assert.AreNotEqual(initialX, newX);
            Assert.AreNotEqual(initialY, newY);
        }

    }
}
