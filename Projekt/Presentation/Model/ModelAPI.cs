using Logic;
using System.Collections.ObjectModel;

namespace Model
{

    public abstract class ModelAPI
    {
        public abstract void Start();
        public abstract void Stop();
        public abstract void CreateBall();
        public abstract ObservableCollection<object> GetBalls();
        public static ModelAPI CreateModelAPI(int boardWidht, int boardHeight,BallsAbstractApi logicAPI)
        {
            if (logicAPI == null)
            {
                return new Model( BallsAbstractApi.CreateApi(boardWidht, boardHeight,null));
            }
            else
            {
                return new Model(logicAPI);
            }
            
        }
    }
    internal class Model : ModelAPI
    {
        private BallsAbstractApi _BallsAPI;

        public Model (BallsAbstractApi _BallsAPI)
        {
            this._BallsAPI = _BallsAPI;
        }

        public override void Start()
        {
            _BallsAPI.Start();
        }

        public override void Stop()
        {
            _BallsAPI.Stop();
        }

        public override void CreateBall()
        {
            _BallsAPI.CreateBall();
        }
        public override ObservableCollection<object> GetBalls()
        {
            ObservableCollection<object> balls = new ObservableCollection<object>();
            foreach (object ball in _BallsAPI.balls)
                balls.Add(ball);
            return balls;
        }
    }

}
