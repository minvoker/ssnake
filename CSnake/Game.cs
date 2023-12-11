using System;
using SplashKitSDK;
using System.IO;
using System.Diagnostics;

namespace CSnake
{
	public class Game
	{
		private const int UpdateInterval = 120; // Millsecs
		private Stopwatch timer;
		private int cellSize;
		private int cellNumber;
        private int score;
        private Snake snake;
        private Food food;
        private Font myFont;

        public Game()
		{  
            timer = new Stopwatch();
			cellSize = 40;
			cellNumber = 15;
			snake = new Snake(cellNumber, cellSize);
			food = new Food(cellNumber, cellSize);
            score = 0;
            myFont = SplashKit.LoadFont("Roboto-Black", "./font/Roboto-Black.ttf");
        }

		public void Update(Window window)
		{
            SplashKit.ProcessEvents();
            HandleInput();

            snake.Move();
            CollisionDetection();

			DrawGame(window);
            SplashKit.RefreshScreen();
		}

		public void Run()
		{
            timer.Start();
            Window window = new Window("CSnake", cellSize * cellNumber, cellSize * cellNumber);

			do
			{
				if (timer.ElapsedMilliseconds >= UpdateInterval)
				{
					timer.Restart();
					Update(window);
				}

			} while (!window.CloseRequested);

			window.Close();
		}

		public void HandleInput()
        {
            if (SplashKit.KeyTyped(KeyCode.DownKey) && snake.Direction.Y != -1)
            {
                snake.Direction = SplashKit.VectorTo(0, 1);
            }
            else if (SplashKit.KeyTyped(KeyCode.UpKey) && snake.Direction.Y != 1)
            {
                snake.Direction = SplashKit.VectorTo(0, -1);
            }
            else if (SplashKit.KeyTyped(KeyCode.LeftKey) && snake.Direction.X != 1)
            {
                snake.Direction = SplashKit.VectorTo(-1, 0);
            }
            else if (SplashKit.KeyTyped(KeyCode.RightKey) && snake.Direction.X != -1)
            {
                snake.Direction = SplashKit.VectorTo(1, 0);
            }
        }

        public void DrawGame(Window window)
		{
            SplashKit.ClearScreen(Color.White);
            snake.Draw();
			food.Draw();
			DrawScore(window);
		}

        public void CollisionDetection()
        {
            Vector2D head = snake.Body[0];

            // Check food collision
            if (snake.HeadPosition.X == food.GetPos.X && snake.HeadPosition.Y == food.GetPos.Y)
            {
                snake.Grow();
                food.Respawn(snake.Body);
            }
            // Check boundary collision
            if (head.X < 0 || head.X >= cellNumber * cellSize ||
            head.Y < 0 || head.Y >= cellNumber * cellSize)
            {
                GameOver();
            }
            // Check if snake collides with itself
            for (int i = 1; i < snake.Body.Count; i++)
            {
                if (head.X == snake.Body[i].X && head.Y == snake.Body[i].Y)
                {
                    GameOver();
                    break;
                }
            }
        }

        public void DrawScore(Window window)
        {
            score = snake.Body.Count - 3;
            int fontSize = 30;
            string scoreText = score.ToString();
            float textWidth = SplashKit.TextWidth(scoreText, myFont, fontSize);
            float x = window.Width - textWidth - 10;
            float y = 10; 
            window.DrawText(scoreText, Color.Black, myFont, fontSize, x, y);
        }

        public void GameOver()
        {
            snake.Reset();
        }
    }
}

