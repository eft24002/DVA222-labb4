using System;
using System.Collections;
using System.Collections.Generic;

// The god-class implementation of the Sliding Puzzle game
public class SlidingPuzzle
{
	const string Goal = "Slide the numbered tiles until they appear in ascending order with the empty space in the final position.";
	int[,] Tiles { get; set; } // 2D array representing the board tiles
	readonly int Size;         // board size (Size x Size)
	readonly int TileWidth;    // width needed to display the largest number nicely
	readonly string EmptyTile; // string representation of the empty space
	int EmptyRow { get; set; } // current row of the empty tile
	int EmptyCol { get; set; } // current column of the empty tile

	// Constructor: initializes the board and empty tile
	public SlidingPuzzle(int size)
	{
		if (size < 2)
			throw new ArgumentException("The board must be at least 2x2!");
		Size = size;
		Tiles = new int[size, size];
		TileWidth = (int)Math.Ceiling(Math.Log10(size * size - 1)); // calculate width for display
		EmptyTile = new string(' ', TileWidth + 2); // empty tile visual
		EmptyCol = EmptyRow = size - 1; // empty tile starts at bottom-right
		// initialize tiles in the winning configuration (ascending order)
		for (int i = 0; i < size; ++i)
			for (int j = 0; j < size; ++j)
				Tiles[i, j] = i * size + j + 1;
	}

	// Directions for sliding the empty space
	enum Direction { Left, Right, Up, Down }

	// Slide the empty space in the specified direction. Returns true if the move was successful
	bool Slide(Direction direction)
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
	void Swap(int row1, int col1, int row2, int col2)
	{
		var tmp = Tiles[row1, col1];
		Tiles[row1, col1] = Tiles[row2, col2];
		Tiles[row2, col2] = tmp;
	}

	// Shuffle the board by sliding the empty tile n times in random directions
	void Shuffle(int n)
	{
		Random random = new Random();
		Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
		while (n > 0)
			if (Slide(directions[random.Next(directions.Length)])) --n; // decrement only on successful move
	}

	// Check if the board is in the winning configuration
	bool Win()
	{
		// empty tile must be in bottom-right
		if (EmptyRow != Size - 1 || EmptyCol != Size - 1)
			return false;
		// all tiles must be in ascending order
		for (int i = 0; i < Size; ++i)
			for (int j = 0; j < Size; ++j)
				if (Tiles[i, j] != i * Size + j + 1) return false;
		return true;
	}

	// Display the board to the console
	public void DisplayBoard()
	{
		for (int i = 0; i < Size; ++i)
		{
			Console.Write("|");
			for (int j = 0; j < Size; ++j)
			{
				if (i == EmptyRow && j == EmptyCol)
					Console.Write(EmptyTile); // show empty space
				else
					Console.Write($" {Tiles[i, j].ToString().PadLeft(TileWidth)} "); // show numbered tile
				Console.Write("|");
			}
			Console.WriteLine();
		}
		Console.WriteLine();
	}

	// Difficulty levels affect the number of shuffle moves
	public enum DifficultyLevel { Dumb = 1, Normal = 10, Hard = 100 };

	// Game state tracking
	enum GameState { Playing, Win, Exit };

	// Main game loop
	public void Play(DifficultyLevel level)
	{
		Shuffle((int)level); // shuffle the board based on difficulty
		GameState state = GameState.Playing;
		Console.Clear();
		Console.WriteLine(Goal + "\n");
		DisplayBoard();
		Console.Write("Move the empty cell with the arrow-keys ");
		while (state == GameState.Playing)
		{
			if (Console.KeyAvailable) // check if user pressed a key
			{
				ConsoleKeyInfo key = Console.ReadKey(true);  // read key without showing it
				switch (key.Key)
				{
					case ConsoleKey.LeftArrow:
						Slide(Direction.Left);
						break;
					case ConsoleKey.RightArrow:
						Slide(Direction.Right);
						break;
					case ConsoleKey.UpArrow:
						Slide(Direction.Up);
						break;
					case ConsoleKey.DownArrow:
						Slide(Direction.Down);
						break;
					case ConsoleKey.Escape: // exit the game
						Console.ResetColor();
						state = GameState.Exit;
						break;
				}

				Console.Clear();
				Console.WriteLine(Goal + "\n");
				DisplayBoard();
				if (Win()) // check win condition after every move
					state = GameState.Win;
				if (state == GameState.Playing)
					Console.Write("Move the empty cell with the arrow-keys ");
			}
		}
		// Game over messages
		if (state == GameState.Win)
			Console.WriteLine("Congrats!");
		else
			Console.WriteLine("GAME OVER!");
	}
}

// Program entry point
class Program
{
	public static void Main(string[] args)
	{
		Random rnd = new Random();
		// create a random 3x3 to 5x5 sliding puzzle
		var g = new SlidingPuzzle(rnd.Next(3, 6));
		g.Play(SlidingPuzzle.DifficultyLevel.Normal);
	}
}
