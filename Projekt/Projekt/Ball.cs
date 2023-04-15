using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    public class Ball : INotifyPropertyChanged
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
            set
            { 
                _x = value;
                OnPropertyChanged();          
            }
        }

        public int Y
        {
            get { return _y; }
            set 
            { 
                _y = value;
                OnPropertyChanged();
            }
        }

        public int Diameter
        {
            get { return _size * 2; }
        }

        public int Size { get => _size; }

        public void MoveBall(int boardWidth, int boardHeight)
        {
            //check collision with board borders
            if (((_x + _deltaX) - _size) < 0 || _x + _deltaX + _size > boardWidth)
            {
                _deltaX = -_deltaX;
            }
            if (((_y + _deltaY) - _size) < 0 || _y + _deltaY + _size > boardHeight)
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


