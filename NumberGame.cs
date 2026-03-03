namespace SlidingPuzzleGame
{
    public class NumberGame : IGame
    {
        private Game<NumberTile> game;
        private Random rnd = new Random();

        public NumberGame()
        {

            // create a random 3x3 to 5x5 sliding puzzle
            var board = new NumberBoard(new Random().Next(3, 6));
            var renderer = new BoardRenderer<NumberTile>();
            game = new Game<NumberTile>(board, renderer, "Slide the numbered tiles until they appear in ascending order with the empty space in the final position.");
        }

        public void Run()
        {
            game.Play(Game<NumberTile>.DifficultyLevel.Normal);
        }
    }
}