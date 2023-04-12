namespace Logic
{
    internal class Ball
    {
        private int _x;
        private int _y;
        private int _deltaX;
        private int _deltaY;
        private readonly int _size;

        public Ball(int _x, int _y, int _deltaX, int _deltaY, int _size)
        {
            this._x = _x;
            this._y = _y;
            this._deltaX = _deltaX;
            this._deltaY = _deltaY;
            this._size = _size;

        }

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int Size { get => _size; }

        public void MoveBall(int boardWidth, int boardHeight)
        {
            //check collision with board borders
            if (_x + _deltaX < 0 || _x + _deltaX > boardWidth - _size)
            {
                _deltaX = -_deltaX;
            }
            if (_y + _deltaY < 0 || _y + _deltaY > boardHeight - _size)
            {
                _deltaY = -_deltaY;
            }
            // move the ball in the set direction
            _x += _deltaX;
            _y += _deltaY;
        }

    }
}


