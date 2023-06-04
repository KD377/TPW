using Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Timers;


namespace Logic
{

    public abstract class BallsAbstractApi
    {
        public abstract List<BallAPI> balls { get; }
        public abstract int BoardWidth { get; }
        public abstract int BoardHeight { get; }
        public abstract void CreateBall();
        public abstract void Start();
        public abstract void Stop();

        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetBallsNumber();


        public static BallsAbstractApi CreateApi(int boardWidth,int boardHeight,DataAPI data)
        {
            if (data == null)
            {
                
                return new BallsAPI(DataAPI.CreateDataAPI(boardWidth,boardHeight));

            }
            else { 
                return new BallsAPI(data);
            }
            
        }
    }
    internal class BallsAPI : BallsAbstractApi
    {
    
        public override List<BallAPI> balls { get; }
        private static readonly ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        public override  int BoardWidth { get; }
        public override  int BoardHeight { get; }

        private DataAPI data;
    

        public BallsAPI(DataAPI data)
        {
            balls = new List<BallAPI>();
            this.data = data;
            this.BoardWidth =  data.getBoardWidth();
            this.BoardHeight = data.getBoardHeight();
 
        }



        public override void CreateBall()
        {
            BallAPI ball = data.createBall(true);
            if(balls.Count <= 0)
            {
                data.StartLogging(DataAPI.Queue);
                ball.isSimulationRunning = true;
            }
            else
            {
                ball.isSimulationRunning = balls[0].isSimulationRunning;
            }
            balls.Add(ball);
            ball.subscribeToPropertyChanged(CheckCollisions);
        }


        private bool CheckCollisionWithOtherBall(BallAPI ball1, BallAPI ball2)
        {
            Vector2 position1 = ball1.Position;
            Vector2 position2 = ball2.Position;
            int distance = (int)Math.Sqrt(Math.Pow((position1.X + ball1.Vx) - (position2.X + ball2.Vx), 2) + Math.Pow((position1.Y + ball1.Vx) - (position2.Y + ball2.Vy), 2));
            if (distance <= ball1.Size / 2 + ball2.Size / 2 )
            {
                readerWriterLockSlim.EnterWriteLock();
                try
                {
                    int v1x = ball1.Vx;
                    int v1y = ball1.Vy;
                    int v2x = ball2.Vx;
                    int v2y = ball2.Vy;

                    int newV1X = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                    int newV1Y = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                    int newV2X = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                    int newV2Y = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
                    ball1.setVelocity(newV1X, newV1Y);
                    ball2.setVelocity(newV2X, newV2Y);
                }
                finally
                {
                    readerWriterLockSlim.ExitWriteLock();
                }
                return false;
            }
            return true;
            
        }

        private void CheckCollisionWithBoard(BallAPI ball)
        {
            readerWriterLockSlim.EnterWriteLock();
            try
            {
                int Vx = ball.Vx;
                int Vy = ball.Vy;
                Vector2 position = ball.Position;

                if (position.X + ball.Vx < 0 || position.X + ball.Vx >= BoardWidth)
                {
                    Vx = -ball.Vx;
                }

                if (position.Y + ball.Vy < 0 || position.Y + ball.Vy >= BoardHeight)
                {
                    Vy = -ball.Vy;
                }

                ball.setVelocity(Vx, Vy);
            }
            finally
            {
                readerWriterLockSlim.ExitWriteLock();
            }
        }

        private void CheckCollisions(object sender,PropertyChangedEventArgs e)
        {
            BallAPI ball = (BallAPI)sender;
            if(ball != null)
            {
                CheckCollisionWithBoard(ball);

                foreach (var ball2 in balls)
                {
                    if (!ball2.Equals(ball))
                    {
                        CheckCollisionWithOtherBall(ball, ball2);
                    }
                }
            }

        }


        public override int GetX(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return (int)balls[index].Position.Y;
            }
            else
            {
                return -1;
            }
        }

        public override int GetY(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return (int)balls[index].Position.X;
            }
            else
            {
                return -1;
            }
        }

        public override int GetBallsNumber()
        {
            return balls.Count;
        }

        public override void Start()
        {
          foreach(var ball in balls)
            {
                ball.isSimulationRunning = true;
            }
            data.StartLogging(BallAPI.BallQueue);
        }

        public override void Stop()
        {
            foreach (var ball in balls)
            {
                ball.isSimulationRunning = false;
            }
            data.StopLogging();
        }

        public override int GetSize(int i)
        {
            if(i>=0 && i < balls.Count)
            {
                return balls[i].Size;
            }
            else
            {
                return -1;
            }
            
        }

     

    }
}