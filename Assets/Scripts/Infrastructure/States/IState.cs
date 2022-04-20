using GameCells;

namespace Infrastructure.States
{
    public interface IState
    {
        void Enter();
        void Exit();
    }

    public interface IMovableState : IState
    {
        void FinishedMove(GameCell gameCell);
    }
    public interface ISwapState : IMovableState
    {
        void Enter(GameCell clickedCell, GameCell activeCell);
    }
}