using System;


namespace SlidingPuzzleGame
{
    public class BoardRenderer<T> : IBoardRenderer<T> where T : ITile
    {
        public void Render(IGameBoard<T> board)
        {
            int size = board.Size;
            int width = (int)Math.Ceiling(Math.Log10(Math.Max(2, size * size)));

            for (int r = 0; r < size; r++)
            {
                Console.Write("|");
                for (int c = 0; c < size; c++)
                {
                    var tile = board.GetTile(r, c);
                    tile.Render(width);
                    Console.Write("|");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}