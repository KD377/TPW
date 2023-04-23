using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public abstract class BallAPI
    {
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Diameter { get; }
        public abstract int Size { get; }
        public abstract void MoveBall(int boardWidth, int boardHeight);
        public static BallAPI CreateBallAPI(int _x, int _y, int _deltaX, int _deltaY, int _size)
        {
            return new Ball(_x, _y, _deltaX, _deltaY, _size);
        }

    }

    internal class Ball : BallAPI, INotifyPropertyChanged
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

        public override int X
        {
            get { return _x; }
            set
            { 
                _x = value;
                OnPropertyChanged();          
            }
        }

        public override int Y
        {
            get { return _y; }
            set 
            { 
                _y = value;
                OnPropertyChanged();
            }
        }

        public override int Diameter
        {
            get { return _size * 2; }
        }

        public override int Size { get => _size; }

        public override void MoveBall(int boardWidth, int boardHeight)
        {
            //check collision with board borders
            if ((_x + _deltaX) < 0 || _x + _deltaX  >= boardWidth)
            {
                _deltaX = -_deltaX;
            }
            if ((_y + _deltaY)  < 0 || _y + _deltaY  >= boardHeight)
            {
                _deltaY = -_deltaY;
            }
            // move the ball in the set direction
            X += _deltaX;
            Y += _deltaY;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

}
}


