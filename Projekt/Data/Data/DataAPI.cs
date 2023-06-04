using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract BallAPI createBall(bool isSimulationRunning);
        public abstract int getBoardWidth();
        public abstract int getBoardHeight();
        public abstract void StopLogging();
        public abstract Task StartLogging(ConcurrentQueue<BallAPI> queue);
        public static ConcurrentQueue<BallAPI> Queue = BallAPI.BallQueue;
        public static DataAPI CreateDataAPI(int boardWidth, int boardHeight)
        {
            return new Data(boardWidth, boardHeight);
        }


    }
    internal class Data : DataAPI
    {
        private int _boardWidth;
        private int _boardHeight;
        private bool run;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly object fileLock = new object(); 

        public Data(int boardWidth, int boardHeight)

        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
        }
        public override int getBoardWidth()
        {
            return _boardWidth;
        }

        public override int getBoardHeight()
        {
            return _boardHeight;
        }

        public override void StopLogging()
        {
            run = false;
        }

        public override async Task StartLogging(ConcurrentQueue<BallAPI> queue)
        {
            run = true;
            await BallLogger(queue);
        }


        private async Task BallLogger(ConcurrentQueue<BallAPI> queue)
        {
            while(run)
            {     
                _stopwatch.Restart();
                queue.TryDequeue(out BallAPI ball);
                if(ball != null)
                 {
                    string log = "{" + String.Format("\n\t\"Date\": \"{0}\",\n\t\"Info\":{1}\n", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff"), JsonSerializer.Serialize(ball)) + "}";

                    lock (fileLock)
                     {
                        using (var writer = new StreamWriter("..\\..\\..\\..\\..\\Log.json", true, Encoding.UTF8))
                        {
                                writer.WriteLine(log);
                        }
                    }
                }
                _stopwatch.Stop();
                await Task.Delay((int)_stopwatch.ElapsedMilliseconds+100);
            }
        }


        public override BallAPI createBall(bool isSimulationRunning)
        {
            Random random = new Random();
            int x = random.Next(20, _boardWidth - 20);
            int y = random.Next(20, _boardHeight - 20);
            int valueX = random.Next(-3, 4);
            int valueY = random.Next(-3, 4);
            Vector2 position = new Vector2((int)x, (int)y);

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
            int mass = 200;
            return BallAPI.CreateBallAPI(position, Vx, Vy, radius, mass,isSimulationRunning);
        }


    }
}