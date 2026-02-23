using System;
using System.Collections;
using System.Collections.Generic;
using SlidingPuzzleGame;

namespace SlidingPuzzleGame
{
    public interface ITile<T> where T : ITile<T>
    {
        int Value { get; set; }
        bool IsEmpty { get; }
    }

    public class NumberTile : ITile<NumberTile>
    {
        public int Value { get; set; }
        public bool IsEmpty => Value == 0; // empty tile has value 0
    }

    //Fixa för färger
    /*public class ColorTile : ITile<ColorTile>
    {
        
    }*/
  
    public abstract class GameBoard<T> where T : ITile<T>    
    {
	    const string Goal = "Slide the numbered tiles until they appear in ascending order with the empty space in the final position.";
	    protected T[,] Tiles { get; set; } // 2D array representing the board tiles
	    protected readonly int Size;         // board size (Size x Size)
	    readonly int TileWidth;    // width needed to display the largest number nicely
	    readonly string EmptyTile; // string representation of the empty space
	    protected int EmptyRow { get; set; } // current row of the empty tile
	    protected int EmptyCol { get; set; } // current column of the empty tile

	    // Constructor: initializes the board and empty tile
	    protected GameBoard(int size)
	    {
		    if (size < 2)
			throw new ArgumentException("The board must be at least 2x2!");
		    Size = size;
		    Tiles = new T[size, size];
		    TileWidth = (int)Math.Ceiling(Math.Log10(size * size - 1)); // calculate width for display
		    EmptyTile = new string(' ', TileWidth + 2); // empty tile visual
		    EmptyCol = EmptyRow = size - 1; // empty tile starts at bottom-right
	    }
    
        // Directions for sliding the empty space
	    public enum Direction { Left, Right, Up, Down }

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
	        T tmp = Tiles[row1, col1];
	        Tiles[row1, col1] = Tiles[row2, col2];
            Tiles[row2, col2] = tmp;
        }
        
        // Shuffle the board by sliding the empty tile n times in random directions
	    public void Shuffle(int n)
	    {
		    Random random = new Random();
		    Direction[] directions = (Direction[])Enum.GetValues(typeof(Direction));
		    while (n > 0)
			    if (Slide(directions[random.Next(directions.Length)])) --n; // decrement only on successful move
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
					    Console.Write($" {Tiles[i, j].Value.ToString().PadLeft(TileWidth)} "); // show numbered tile
				    Console.Write("|");
			    }
			    Console.WriteLine();
		    }
		    Console.WriteLine();
	    }

       public abstract bool Win();
    }

//--------------------------------------------------

    public class NumberBoard : GameBoard<NumberTile>
    {
        public NumberBoard(int size) : base(size)
        {
            // initialize tiles in the winning configuration (ascending order)
            for (int i = 0; i < size; ++i)
			    for (int j = 0; j < size; ++j)
			    	Tiles[i, j] = new NumberTile
                    {
                        Value = (i == size - 1 && j == size - 1) ? 0 : i * size + j + 1
                    }; // last tile is empt
        }

        public override bool Win()
        {
            // empty tile must be in bottom-right
		    if (EmptyRow != Size - 1 || EmptyCol != Size - 1)
			    return false;
		    // all tiles must be in ascending order
		    for (int i = 0; i < Size; ++i)
		    	for (int j = 0; j < Size; ++j)
		    		if (Tiles[i, j].Value != i * Size + j + 1) return false;
		    return true;
	    }		 
    }

//-----------------------------------------------
 //en klass för färger som en ovan 
    /*public class ColorBoard : GameBoard<ColorTile>
    {
        public ColorBoard(int size) : base(size)
        {
 
        }

        public override bool Win()
        {

        }		 
    }*/
//-----------------------------------------------
    public class Game 
    {
        private GameBoard<NumberTile> board;

        const string Goal = "Slide the numbered tiles until they appear in ascending order with the empty space in the final position.";
    
    	// Difficulty levels affect the number of shuffle moves
	    public enum DifficultyLevel { Dumb = 1, Normal = 10, Hard = 100 };
	    // Game state tracking
	    public enum GameState { Playing, Win, Exit };

        public Game(GameBoard<NumberTile> board) 
        {
            this.board = board;
        }

        public void Play(DifficultyLevel level)
	    {
		    board.Shuffle((int)level); // shuffle the board based on difficulty
		    GameState state = GameState.Playing;
		    Console.Clear();
		    Console.WriteLine(Goal + "\n");
		    board.DisplayBoard();
		    Console.Write("Move the empty cell with the arrow-keys ");
		
            while (state == GameState.Playing)
		    {
			    if (Console.KeyAvailable) // check if user pressed a key
			    {
				    ConsoleKeyInfo key = Console.ReadKey(true);  // read key without showing it
				    switch (key.Key)
				    {
					    case ConsoleKey.LeftArrow:
						    board.Slide(GameBoard<NumberTile>.Direction.Left);
						    break;
					    case ConsoleKey.RightArrow:
						    board.Slide(GameBoard<NumberTile>.Direction.Right);
						    break;
					    case ConsoleKey.UpArrow:
					    	board.Slide(GameBoard<NumberTile>.Direction.Up);
					    	break;
					    case ConsoleKey.DownArrow:
					    	board.Slide(GameBoard<NumberTile>.Direction.Down);
					    	break;
					    case ConsoleKey.Escape: // exit the game
					    	Console.ResetColor();
					    	state = GameState.Exit;
					    	break;
				    }

				    Console.Clear();
				    Console.WriteLine(Goal + "\n");
				    board.DisplayBoard();
				
                    if (board.Win()) // check win condition after every move
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
//----------------------------------------------
    // Program entry point
    class Program
    {
	    public static void Main(string[] args)
	    {
		    Random rnd = new Random();

            Console.WriteLine("Welcome to the Sliding Puzzle Game!\n choose 1 for numbers, 2 for colors");
            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
		        // create a random 3x3 to 5x5 sliding puzzle
		        var board = new NumberBoard(rnd.Next(3, 6));
                var game = new Game(board);
		        game.Play(Game.DifficultyLevel.Normal);
            }
            /*else if(choice == 2)
            {
                // create a random 3x3 to 5x5 sliding puzzle
                var g = new ColorBoard(rnd.Next(3, 6));
		        g.Game.Play(ColorBoard.DifficultyLevel.Normal);
            }*/
            else
            {
                Console.WriteLine("Wrong input:(");
            }
    	}
	}
}
