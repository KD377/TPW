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
       // public abstract void MoveBall(object sender, EventArgs e);
        public abstract void Start();
        public abstract void Stop();
        //public abstract void SetPeriod(int ms_period);

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

        //private DataAPI data;

        public BallsAPI(int widht, int height)
        {
            balls = new List<Ball>();
            Timer = new System.Timers.Timer(1000); // 60 FPS
            Timer.Elapsed += OnTimerTick;
            this.BoardWidth = widht;
            this.BoardHeight = height;
           // data = DataAPI.CreateDataAPI();
 
        }

        public override void CreateBall()
        {
            Random random = new Random();
            int x = random.Next(0, BoardWidth);
            int y = random.Next(0, BoardHeight);
            int Vx = random.Next(-2, 3);
            int Vy = random.Next(-2, 3);
            int radius = 5;
            balls.Add(new Ball(x, y, Vx, Vy, radius));
        }


        public void OnTimerTick(object sender, ElapsedEventArgs e)
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

        /*public override void SetPeriod(int ms_period)
        {
            Timer.Interval = TimeSpan.FromMilliseconds(ms_period);
        }*/


    }
}