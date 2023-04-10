using System;
namespace Logic
{
    public class BallAPI
    {
        private readonly Ball _ball;

        public BallAPI(int x, int y, int size)
        {
            _ball = new Ball(x, y, size);
        }

        public int GetX()
        {
            return _ball.X;
        }

        public void SetX(int value)
        {
            _ball.X = value;
        }

        public int GetY()
        {
            return _ball.Y;
        }

        public void SetY(int value)
        {
            _ball.Y = value;
        }

        public int GetSize()
        {
            return _ball.Size;
        }
    }

}

