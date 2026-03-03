using System;


namespace SlidingPuzzleGame
{
    public interface ITile
    {
        bool IsEmpty { get; }
        void Render(int width);
    }

    public interface IGameBoard<TTile> where TTile : ITile
    {
        int Size { get; }
        void Shuffle(int moves);
        bool Slide(Direction direction);
        bool IsWin();
        TTile GetTile(int row, int col);
    }

    public interface IBoardRenderer<TTile> where TTile : ITile
    {
        void Render(IGameBoard<TTile> board);
    }

    public interface IGame
    {
        void Run();
    }
}
