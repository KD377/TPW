using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class BallsAPITests
    {
        private BallsAbstractApi ballsApi;

        [TestInitialize]
        public void TestInitialize()
        {
            ballsApi = BallsAbstractApi.CreateApi(800, 600);
        }

        [TestMethod]
        public void TestCreateBall()
        {
            ballsApi.CreateBall();
            Assert.AreEqual(1, ballsApi.GetBallsNumber());

          
            int x = ballsApi.GetX(0);
            int y = ballsApi.GetY(0);
            int size = ballsApi.GetSize(0);
            Assert.IsTrue(x >= 20 && x <= 780);
            Assert.IsTrue(y >= 20 && y <= 580);
            Assert.AreEqual(20, size);

        }

        [TestMethod]
        public void TestMoveBall()
        {
        
            ballsApi.CreateBall();
            int x0 = ballsApi.GetX(0);
            int y0 = ballsApi.GetY(0);

            
            ballsApi.Start();
            System.Threading.Thread.Sleep(100);
            ballsApi.Stop();

            int x1 = ballsApi.GetX(0);
            int y1 = ballsApi.GetY(0);
            Assert.AreNotEqual(x0, x1);
            Assert.AreNotEqual(y0, y1);
        }

        [TestMethod]
        public void TestBounds()
        {
          
            ballsApi.CreateBall();

            ballsApi.Start();
            System.Threading.Thread.Sleep(10000);
            ballsApi.Stop();

            int x = ballsApi.GetX(0);
            int y = ballsApi.GetY(0);
            int size = ballsApi.GetSize(0);

            Assert.IsTrue(x >= size && x <= ballsApi.BoardWidth - size);
            Assert.IsTrue(y >= size && y <= ballsApi.BoardHeight - size);
        }

    }
}
