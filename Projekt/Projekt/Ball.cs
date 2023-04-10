namespace Logic
{
    internal class Ball
    {
        private int _x;
        private int _y;
        private readonly int _size;

        public Ball(int _x, int _y, int _size)
        {
            this._x = _x;
            this._y = _y;
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

    }
}


