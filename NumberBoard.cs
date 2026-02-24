namespace SlidingPuzzleGame
{
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
                    }; // last tile is empty
        }

        public override bool IsWin()
        {
            // empty tile must be in bottom-right
            if (EmptyRow != Size - 1 || EmptyCol != Size - 1)
                return false;
            // all tiles must be in ascending order
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    int expectedValue = (i == Size - 1 && j == Size - 1) ? 0 : i * Size + j + 1;
                    if (Tiles[i, j].Value != expectedValue)
                     return false;
                }
            }
            
            return true;
        }		 
    }

    // En klass för färger som en ovan 
    /*public class ColorBoard : GameBoard<ColorTile>
    {
        public ColorBoard(int size) : base(size)
        {
 
        }

        public override bool Win()
        {

        }		 
    }*/
}
