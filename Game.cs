using System;

namespace SlidingPuzzleGame
{
    public sealed class Game<TTile> where TTile : ITile
    {        
        private readonly IGameBoard<TTile> _board;
        private readonly IBoardRenderer<TTile> _renderer;
        private readonly string _goalText;

        // Difficulty levels affect the number of shuffle moves
        public enum DifficultyLevel { Dumb = 1, Normal = 10, Hard = 100 };
        // Game state tracking
        public enum GameState { Playing, Win, Exit };

        public Game(IGameBoard<TTile> board, IBoardRenderer<TTile> renderer, string goalText) 
        {
            _board = board;
            _renderer = renderer;
            _goalText = goalText;
        }

        public void Play(DifficultyLevel level)
        {
            _board.Shuffle((int)level); // shuffle the board based on difficulty
            GameState state = GameState.Playing;
            Console.Clear();
            Draw();
        
            while (state == GameState.Playing)
            {
                if (!Console.KeyAvailable) continue; // check if user pressed a key
                
                var key = Console.ReadKey(true);

                switch(key.Key)
                {
                    case ConsoleKey.LeftArrow: _board.Slide(Direction.Left); break;
                    case ConsoleKey.RightArrow: _board.Slide(Direction.Right); break;
                    case ConsoleKey.UpArrow: _board.Slide(Direction.Up); break;
                    case ConsoleKey.DownArrow: _board.Slide(Direction.Down); break;
                    case ConsoleKey.Escape: Console.ResetColor(); state = GameState.Exit; break;
                }
                Console.Clear();
                Draw();

                if (state == GameState.Playing && _board.IsWin())
                    state = GameState.Win;
            }

            Console.ResetColor();
            Console.WriteLine(state == GameState.Win ? "Congrats!" : "GAME OVER!");
        }

        private void Draw()
        {
            Console.WriteLine(_goalText);
            Console.WriteLine();
            _renderer.Render(_board);
            Console.WriteLine("Use arrow keys to slide tiles, Esc to exit.");
        }
    }
}
