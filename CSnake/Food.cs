using System;
using SplashKitSDK;
namespace CSnake
{
	public class Food
	{
	private Random r = new Random();
	private Vector2D pos;
	private int cellNumber;
        private int cellSize;

        public Food(int cellNumber, int cellSize)
        {
            this.cellNumber = cellNumber;
            this.cellSize = cellSize;
            pos = new Vector2D();
            RandomPos();
        }

	public void RandomPos()
	{
            int x = r.Next(0, cellNumber);
            int y = r.Next(0, cellNumber);
            pos.X = x * cellSize;
            pos.Y = y * cellSize;
        }

	public void Draw()
	{
            int foodSize = cellSize;
            SplashKit.FillRectangle(Color.Red, pos.X, pos.Y, foodSize, foodSize);
        }

        public void Respawn(List<Vector2D> snakeBody)
        {
            while(true) // Randomises pos until valid spawn is found
            {
                RandomPos();
                bool validSpawn = true;

                foreach(Vector2D i in snakeBody)
                {
                    if(i.X == pos.X && i.Y == pos.Y)
                    {
                        validSpawn = false;
                        break;
                    }
                }

                if(validSpawn)
                {
                    break;
                }
            }
        }

        public Vector2D GetPos { get { return pos; } }
    }
}

