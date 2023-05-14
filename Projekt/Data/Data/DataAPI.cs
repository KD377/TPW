using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public abstract class DataAPI
    {
        public abstract BallAPI createBall(CancellationTokenSource cancellationTokenSource);
        public abstract int getBoardWidth();
        public abstract int getBoardHeight();
        public static DataAPI CreateDataAPI(int boardWidth, int boardHeight)
        {
            return new Data(boardWidth, boardHeight);
        }


    }
    internal class Data : DataAPI
    {
        private int _boardWidth;
        private int _boardHeight;

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

        public override BallAPI createBall(CancellationTokenSource cancellationTokenSource)
        {
            Random random = new Random();
            int x = random.Next(20, _boardWidth - 20);
            int y = random.Next(20, _boardHeight - 20);
            int valueX = random.Next(-3, 4);
            int valueY = random.Next(-3, 4);
            Vector2 position = new Vector2((int)x, (int)y);
            Debug.WriteLine(position);

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
            return BallAPI.CreateBallAPI(position, Vx, Vy, radius, mass, cancellationTokenSource);
        }


    }
}
