using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace CSnake
{
    public class Snake
    {
        private List<Vector2D> snakeBody;
        private Vector2D direction;
        private bool newSegment = false;
        private int cellNumber;
        private int cellSize;

        public Snake(int cellNumber, int cellSize)
        {
            this.cellSize = cellSize;
            this.cellNumber = cellNumber;
            snakeBody = new List<Vector2D>();
            InitializeSnake();
            direction = SplashKit.VectorTo(1, 0);
        }

        private void InitializeSnake()
        {
            int midGridX = cellNumber / 2 * cellSize;
            int midGridY = cellNumber / 2 * cellSize;

            for (int i = 0; i < 3; i++)
            {
                Vector2D starterSegment = SplashKit.VectorTo(midGridX - i * cellSize, midGridY);
                snakeBody.Add(starterSegment);
            }
        }

        public void Draw()
        {
            foreach (Vector2D segment in snakeBody)
            {
                SplashKit.FillRectangle(Color.Green, segment.X, segment.Y, cellSize, cellSize);
            }
        }

        public void Move()
        {
            Vector2D newHead = SplashKit.VectorTo(
                snakeBody[0].X + direction.X * cellSize,
                snakeBody[0].Y + direction.Y * cellSize);

            if (newSegment)
            {
                snakeBody.Insert(0, newHead);
                newSegment = false;
            }
            else
            {
                snakeBody.RemoveAt(snakeBody.Count - 1);
                snakeBody.Insert(0, newHead);
            }
        }

        public void Grow()
        {
            newSegment = true;
        }

        public void Reset()
        {
            snakeBody.Clear();
            InitializeSnake();
            direction = SplashKit.VectorTo(1, 0); 
            newSegment = false;
        }

        public Vector2D Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public List<Vector2D> Body
        {
            get { return snakeBody; }
        }

        public Vector2D HeadPosition
        {
            get { return snakeBody[0]; }
        }
    }
}