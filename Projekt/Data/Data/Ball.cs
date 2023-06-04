using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Data
{
    public abstract class BallAPI
    {
        public abstract Vector2 Position { get; }

        public abstract int X { get; }
        public abstract int Y { get; }

        public abstract bool isSimulationRunning { get; set; }

        public abstract void subscribeToPropertyChanged(PropertyChangedEventHandler handler);

        public abstract void setVelocity(int Vx, int Vy);

        public abstract int Diameter { get; }

        public abstract int Vx { get;  }

        public abstract int Vy { get; }

        public abstract int Mass { get; }
        public abstract int Size { get; }
        public static ConcurrentQueue<BallAPI> BallQueue { get; } = new ConcurrentQueue<BallAPI>();

        public static BallAPI CreateBallAPI(Vector2 _position, int _deltaX, int _deltaY, int _size, int _mass,bool isSimulationRunning)
        {
            return new Ball(_position, _deltaX, _deltaY, _size, _mass,isSimulationRunning);
        }

    }



    internal class Ball : BallAPI, INotifyPropertyChanged
    {
        private Vector2 _position;
        private bool _isSimulationRunning;
        private int _deltaX;
        private int _deltaY;
        private readonly int _size;
        private readonly int _mass;
        private static readonly ReaderWriterLockSlim velocityLock = new ReaderWriterLockSlim();
        private static readonly ReaderWriterLockSlim positionLock = new ReaderWriterLockSlim();
        private readonly Stopwatch stopwatch = new Stopwatch();


        public Ball(Vector2 position, int deltaX, int deltaY, int size, int mass, bool _isSimulationRunning)
        {
            _position = position;
            _deltaX = deltaX;
            _deltaY = deltaY;
            _size = size;
            _mass = mass;
            Task.Run(() => Move());
            this.isSimulationRunning = _isSimulationRunning;
        }

        public override Vector2 Position
        {
            get 
            {
                positionLock.EnterReadLock();
                try
                {
                    return _position; 
                }
                finally { positionLock.ExitReadLock(); }
                
            }
        }

        public override int X { get { return (int)_position.X; } }
        public override int Y { get { return (int)_position.Y; } }

        public override void subscribeToPropertyChanged(PropertyChangedEventHandler handler)
        {
            this.PropertyChanged += handler;
        }


        private void setPosition(Vector2 newPosition)
        {
            positionLock.EnterWriteLock();
            try
            {
                _position.X = newPosition.X;
                _position.Y = newPosition.Y;
            }
            finally
            {
                positionLock.ExitWriteLock();
            }
            OnPropertyChanged(nameof(Position.X));
            OnPropertyChanged(nameof(Position.Y));
        }

        public override bool isSimulationRunning 
        {
            get { return _isSimulationRunning; }
            set { _isSimulationRunning = value; } 
        }



        public override int Vx
        {
            get 
            {
                velocityLock.EnterReadLock();
                try
                {
                    return _deltaX;
                }
                finally { velocityLock.ExitReadLock(); } 
            }
        }

        public override int Vy
        {
            get
            {
                velocityLock.EnterReadLock();
                try
                {
                    return _deltaY;
                }
                finally { velocityLock.ExitReadLock(); }
            }
        }

        public override void setVelocity(int Vx, int Vy)
        {
            velocityLock.EnterWriteLock();
            try
            {
                this._deltaX = Vx;
                this._deltaY = Vy;
            }
            finally
            {
                velocityLock?.ExitWriteLock();
            }
           
        }

        public override int Diameter => _size * 2;
        public override int Mass => _mass;
        public override int Size => _size;

        private async Task Move()
        {
            while (true)
            {
                stopwatch.Restart();
                if (isSimulationRunning)
                {
                    int newX = (int)_position.X + _deltaX;
                    int newY = (int)_position.Y + _deltaY;
                    Vector2 newPosition = new Vector2(newX, newY);
                    setPosition(newPosition);
                    BallQueue.Enqueue(this);
                }
                
                double velocity = Math.Sqrt(_deltaX * _deltaX + _deltaY * _deltaY);
                stopwatch.Stop();
                await Task.Delay(TimeSpan.FromMilliseconds(1000 / 460 * velocity+(int)stopwatch.ElapsedMilliseconds));
            }


        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
