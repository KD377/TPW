using Data;
using System;
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
        public override  int BoardWidth { get; }
        public override  int BoardHeight { get; }

        private DataAPI data;
        private CancellationTokenSource cancellationTokenSource = null;
    

        public BallsAPI(DataAPI data)
        {
            balls = new List<BallAPI>();
            this.data = data;
            this.BoardWidth =  data.getBoardWidth();
            this.BoardHeight = data.getBoardHeight();
 
        }

        private bool isBallSpawned(BallAPI ball1,BallAPI ball2)
        {
            Vector2 position1 = ball1.Position;
            Vector2 position2 = ball2.Position;
            int distance = (int)Math.Sqrt(Math.Pow((position1.X + ball1.Vx) - (position2.X + ball2.Vx), 2) + Math.Pow((position1.Y + ball1.Vx) - (position2.Y + ball2.Vy), 2));
            if (distance <= ball1.Size / 2 + ball2.Size / 2)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public override void CreateBall()
        {
            BallAPI ball = data.createBall(cancellationTokenSource);
            balls.Add(ball);
            ball.subscribeToPropertyChanged(CheckCollisions);
           
        }

        private void CheckCollisionWithOtherBall(BallAPI ball1, BallAPI ball2)
        {
            Vector2 position1 = ball1.Position;
            Vector2 position2 = ball2.Position;
            int distance = (int)Math.Sqrt(Math.Pow((position1.X + ball1.Vx) - (position2.X + ball2.Vx), 2) + Math.Pow((position1.Y + ball1.Vx) - (position2.Y + ball2.Vy), 2));
            if (distance <= ball1.Size / 2 + ball2.Size / 2 )
            {
                // collision detected
                int v1x = ball1.Vx;
                int v1y = ball1.Vy;
                int v2x = ball2.Vx;
                int v2y = ball2.Vy;

                ball1.Vx = (v1x * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2x) / (ball1.Mass + ball2.Mass);
                ball1.Vy = (v1y * (ball1.Mass - ball2.Mass) + 2 * ball2.Mass * v2y) / (ball1.Mass + ball2.Mass);
                ball2.Vx = (v2x * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1x) / (ball1.Mass + ball2.Mass);
                ball2.Vy = (v2y * (ball2.Mass - ball1.Mass) + 2 * ball1.Mass * v1y) / (ball1.Mass + ball2.Mass);
            }
            
        }

        private void CheckCollisionWithBoard(BallAPI ball)
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
                ball.CancellationTokenSource = new CancellationTokenSource();
                ball.Start();
            }
        }

        public override void Stop()
        {
            foreach (var ball in balls)
            {
                ball.CancellationTokenSource.Cancel();
            }
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