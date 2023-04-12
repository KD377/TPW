﻿using System;
using System.Collections.Generic;

namespace Logic
{
    public class BallsAPI
    {
        private readonly TimerApi Timer;
        private List<Ball> Balls;
        private readonly int BoardWidth;
        private readonly int BoardHeight;
        public BallsAPI(int widht, int height)
        {
            Balls = new List<Ball>();
            Timer = TimerApi.CreateBallTimer();
            this.BoardWidth = widht;
            this.BoardHeight = height;

            Timer.Tick += MoveBalls;
        }

        public void CreateBalls(int counter)
        {

            if (counter >= 0)
            {
                int radius = 20;
                Random random = new Random();
                for (int i = 0; i < counter; i++)
                {
                    int x = random.Next(radius, BoardWidth - radius);
                    int y = random.Next(radius, BoardHeight - radius);
                    int deltaX = random.Next(-10, 10 + 1);
                    int deltaY = random.Next(-10, 10 + 1);
                    Ball ball = new Ball(x, y, deltaX, deltaY, radius);
                    Balls.Add(ball);

                }
            }
        }

        public int GetX(int index)
        {
            if (index >= 0 && index < Balls.Count)
            {
                return Balls[index].X;
            }
            else
            {
                return -1;
            }
        }

        public int GetY(int index)
        {
            if (index >= 0 && index < Balls.Count)
            {
                return Balls[index].Y;
            }
            else
            {
                return -1;
            }
        }

        public int GetBallsNumber()
        {
            return Balls.Count;
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        public void setPeriod(int ms_period)
        {
            Timer.Interval = TimeSpan.FromMilliseconds(ms_period);
        }

        private void MoveBalls(object sender, EventArgs e)
        {

            foreach (var ball in Balls)
            {
                ball.MoveBall(BoardWidth, BoardHeight);
            }
        }

    }
}