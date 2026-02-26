using System;
using SlidingPuzzleGame;

// Program entry point
 class Program
{
    public static void Main(string[] args)
    {
        Random rnd = new Random();

        Console.WriteLine("Welcome to the Sliding Puzzle Game!\nChoose 1 for numbers, 2 for colors");
        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
            // create a random 3x3 to 5x5 sliding puzzle
            var board = new NumberBoard(rnd.Next(3, 6));
            var renderer = new BoardRenderer<NumberTile>();
            var game = new Game<NumberTile>(board, renderer, "Slide the numbered tiles until they appear in ascending order with the empty space in the final position.");
            game.Play(Game<NumberTile>.DifficultyLevel.Normal);
        }
        else if(choice == 2)
        {
            // create a random 3x3 to 5x5 sliding puzzle
            var g = new ColorBoard(rnd.Next(3, 6));
            var renderer = new BoardRenderer<ColorTile>();
            var game = new Game<ColorTile>(g, renderer, "Slide the colored tiles until they appear in ascending order with the empty space in the final position.");
            game.Play(Game<ColorTile>.DifficultyLevel.Normal);
        }
        else
        {
            Console.WriteLine("Wrong input:(");
        }
    }
}