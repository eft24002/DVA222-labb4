using System;

namespace SlidingPuzzleGame
{
    public abstract class GameBoard<TTile> : IGameBoard<TTile> where TTile : ITile    
    {
        protected TTile[,] Tiles { get; set; } // 2D array representing the board tiles
        public int Size { get; }         // board size (Size x Size)
        protected int EmptyRow { get; set; } // current row of the empty tile
        protected int EmptyCol { get; set; } // current column of the empty tile
        private readonly Random _random = new Random();

        // Constructor: initializes the board and empty tile
        protected GameBoard(int size)
        {
            if (size < 2)
                throw new ArgumentException("The board must be at least 2x2!");
            Size = size;
            Tiles = new TTile[size, size];
            EmptyCol = EmptyRow = size - 1; // empty tile starts at bottom-right
        }

        // Slide the empty space in the specified direction. Returns true if the move was successful
        public bool Slide(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (EmptyCol == 0) return false; // cannot move left
                    Swap(EmptyRow, EmptyCol, EmptyRow, --EmptyCol); // swap with left tile
                    return true;
                case Direction.Right:
                    if (EmptyCol == Size - 1) return false; // cannot move right
                    Swap(EmptyRow, EmptyCol, EmptyRow, ++EmptyCol); // swap with right tile
                    return true;
                case Direction.Up:
                    if (EmptyRow == 0) return false; // cannot move up
                    Swap(EmptyRow, EmptyCol, --EmptyRow, EmptyCol); // swap with above tile
                    return true;
                case Direction.Down:
                    if (EmptyRow == Size - 1) return false; // cannot move down
                    Swap(EmptyRow, EmptyCol, ++EmptyRow, EmptyCol); // swap with below tile
                    return true;
                default:
                    throw new ArgumentException("Unexpected direction");
            }
        }

        // Swap the values of two tiles on the board
        private void Swap(int row1, int col1, int row2, int col2)
        {
            TTile tmp = Tiles[row1, col1];
            Tiles[row1, col1] = Tiles[row2, col2];
            Tiles[row2, col2] = tmp;
        }
        
        // Shuffle the board by sliding the empty tile n times in random directions
        public void Shuffle(int n)
        {
            Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
            while (n > 0)
                if (Slide(directions[_random.Next(directions.Length)])) --n; // decrement only on successful move
        }

        public TTile GetTile(int row, int col) => Tiles[row, col];
        public abstract bool IsWin();
    }
}
