namespace SlidingPuzzleGame
{
    public class ColorTile : ITile
    {
        public ConsoleColor color { get; set; }
        public bool IsEmpty => color == ConsoleColor.Black; 

        public ColorTile(ConsoleColor color)
        {
            this.color = color;
        }
        public void Render(int width)
        {
            Console.BackgroundColor = color;
            Console.Write(new string(' ', width + 2));
            Console.ResetColor();
        }
    }
}
