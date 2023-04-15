using Data;
using System;
using System.Collections.Generic;
using System.Timers;


namespace Logic
{

    public abstract class BallsAbstractApi
    {
        public abstract List<Ball> balls { get; }
        public abstract int BoardWidth { get; }
        public abstract int BoardHeight { get; }
        public abstract void CreateBall();
        public abstract void Start();
        public abstract void Stop();

        public abstract int GetX(int i);
        public abstract int GetY(int i);
        public abstract int GetSize(int i);
        public abstract int GetBallsNumber();

        public static BallsAbstractApi CreateApi(int width, int height)
        {
            return new BallsAPI(width, height);
        }
    }
    public class BallsAPI : BallsAbstractApi
    {
        public System.Timers.Timer Timer;
        public override List<Ball> balls { get; }
        public override  int BoardWidth { get; }
        public override  int BoardHeight { get; }

        private DataAPI data;

        public BallsAPI(int widht, int height)
        {
            balls = new List<Ball>();
            Timer = new System.Timers.Timer(1000/60); 
            Timer.Elapsed += OnTimerTick;
            this.BoardWidth = widht;
            this.BoardHeight = height;
            data = DataAPI.CreateDataAPI();
 
        }

        public override void CreateBall()
        {
            Random random = new Random();
            int x = random.Next(20, BoardWidth-20);
            int y = random.Next(20, BoardHeight-20);
            int valueX = random.Next(-2, 3);
            int valueY = random.Next(-2, 3);

            if (valueX == 0)
            {
                valueX = random.Next(1, 3) * 2 - 3;
            }
            if (valueY == 0)
            {
                valueY = random.Next(1, 3) * 2 - 3;
            }

            int Vx = valueX;
            int Vy = valueY;
            int radius = 20;
            balls.Add(new Ball(x, y, Vx, Vy, radius));
        }


        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            foreach (var ball in balls)
            {
                ball.MoveBall(BoardWidth, BoardHeight);
            }
        }
        public override int GetX(int index)
        {
            if (index >= 0 && index < balls.Count)
            {
                return balls[index].X;
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
                return balls[index].Y;
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
            Timer.Start();
        }

        public override void Stop()
        {
            Timer.Stop();
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