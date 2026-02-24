using System;

namespace SlidingPuzzleGame
{
    public class NumberBoardRenderer : IBoardRenderer<NumberTile>
    {
        public void Render(IGameBoard<NumberTile> board)
        {
            int size = board.Size;
            int width = (int)Math.Ceiling(Math.Log10(Math.Max(2, size * size)));

            for (int r = 0; r < size; r++)
            {
                Console.Write("|");
                for (int c = 0; c < size; c++)
                {
                    var tile = board.GetTile(r, c);

                    if (tile.IsEmpty)
                    {
                        Console.Write(new string(' ', width + 2));
                    }
                    else
                    {
                        Console.Write($" {tile.Value.ToString().PadLeft(width)} ");
                    }

                    Console.Write("|");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
    }
}