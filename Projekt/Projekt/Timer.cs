using System;
using System.Threading;

namespace Logic
{
    public abstract class TimerApi
    {
        public abstract event EventHandler Tick;
        public abstract TimeSpan Interval { get; set; }
        public abstract void Start();
        public abstract void Stop();
        public static TimerApi CreateBallTimer()
        {
            return new BallTimer();
        }
    }

    internal class BallTimer : TimerApi
    {
        private Thread timerThread;
        private bool isRunning;

        public BallTimer()
        {
            timerThread = new Thread(new ThreadStart(OnTick));
            isRunning = false;
        }

        private void OnTick()
        {
            while (isRunning)
            {
                Tick?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(Interval);
            }
        }

        public override TimeSpan Interval { get; set; }

        public bool IsRunning()
        {
            return isRunning;
        }

        public override event EventHandler Tick;

        public override void Start()
        {
            if (!isRunning)
            {
                isRunning = true;
                timerThread.Start();
            }
        }

        public override void Stop()
        {
            if (isRunning)
            {
                isRunning = false;
                timerThread.Join();
            }
        }
    }
}
