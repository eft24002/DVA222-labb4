using System;

namespace SlidingPuzzleGame
{
    public interface ITile
    {
        bool IsEmpty { get; }
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
}
