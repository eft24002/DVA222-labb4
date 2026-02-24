namespace SlidingPuzzleGame
{
    public class NumberTile : ITile
    {
        public int Value { get; set; }
        public bool IsEmpty => Value == 0; // empty tile has value 0
        public void Render(int width)
        {
            if (IsEmpty)
            {
                Console.Write(new string(' ', width + 2));
            }
            else
            {
                Console.Write($" {Value.ToString().PadLeft(width)} ");
            }
        }
    }
}
