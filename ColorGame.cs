namespace SlidingPuzzleGame
{
    public class ColorGame : IGame
    {
        private  Game<ColorTile> game;
        private Random rnd =new Random();
        public ColorGame()
        {
            // create a random 3x3 to 5x5 sliding puzzle
            var board = new ColorBoard(new Random().Next(3, 6));
            var renderer = new BoardRenderer<ColorTile>();
            game = new Game<ColorTile>(board, renderer, "Slide the colored tiles until they appear in ascending order with the empty space in the final position.");
        }

        public void Run()
        {
            game.Play(Game<ColorTile>.DifficultyLevel.Normal);
        }
    }
}