namespace SlidingPuzzleGame
{
    public class NumberTile : ITile
    {
        public int Value { get; set; }
        public bool IsEmpty => Value == 0; // empty tile has value 0
    }

    // Fixa för färger
    /*public class ColorTile : ITile
    {
        
    }*/
}
