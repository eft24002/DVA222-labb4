using System.Drawing;

namespace SlidingPuzzleGame
{
    public class ColorBoard : GameBoard<ColorTile>
    {
        ConsoleColor[] colors = {ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.Magenta};
        public ColorBoard(int size) : base(size)
        {
            // initialize tiles in the winning configuration (ascending order)
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                {
                    Tiles[i, j] = (i == size - 1 && j == size - 1)
                        ? new ColorTile(ConsoleColor.Black)
                        : new ColorTile(colors[(i * size + j) % (colors.Length - 1)]);
                } 
        }

        public override bool IsWin()
        {
            var colorSet = new List<ConsoleColor>();
            var color = ConsoleColor.Black;

            // empty tile must be in bottom-right
            if (EmptyRow != Size - 1 || EmptyCol != Size - 1)
                return false;
            
            // collect all colors in order
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    var currentColor = Tiles[i, j].color;
                    
                    if(currentColor == ConsoleColor.Black) continue;

                    if(currentColor != color)
                    {
                        color = currentColor;
                        if(colorSet.Contains(currentColor)) return false;
 
                        colorSet.Add(currentColor);
                    }
                }   
            
            }		 
            return true;
        }
    }    
}
