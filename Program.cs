using System;
using SlidingPuzzleGame;

// Program entry point
 class Program
{
    public static void Main(string[] args)
    {
        IGame game;

        Console.WriteLine("Welcome to the Sliding Puzzle Game!\nChoose 1 for numbers, 2 for colors");
        int choice = Convert.ToInt32(Console.ReadLine());

        if (choice == 1)
        {
           game = new NumberGame();
        }
        else
        {
            game = new ColorGame();
        }
        game.Run();
    }
}