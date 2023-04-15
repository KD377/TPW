using Logic;
using System.Collections.ObjectModel;

namespace Model
{
    public class ModelAPI
    {
        private BallsAbstractApi _BallsAPI;

        public ModelAPI(int boardWidth, int boardHeight)
        {
            _BallsAPI = BallsAbstractApi.CreateApi(boardWidth, boardHeight);
        }

        public void Start()
        {
            _BallsAPI.Start();
        }

        public void Stop()
        {
            _BallsAPI.Stop();
        }

        public void CreateBall()
        {
            _BallsAPI.CreateBall();
        }
        public ObservableCollection<Ball> GetBalls()
        {
            ObservableCollection<Ball> balls = new ObservableCollection<Ball>();
            foreach (Ball ball in _BallsAPI.balls)
                balls.Add(ball);
            return balls;
        }
    }

}
