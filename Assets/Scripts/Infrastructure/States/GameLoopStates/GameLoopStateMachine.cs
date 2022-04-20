using GameCells;
using Infrastructure.States.GameStates;

namespace Infrastructure.States.GameLoopStates
{
    public class GameLoopStateMachine
    {
        private IState activeState;
        public StatesContainer container;
        
        public GameLoopStateMachine(StatesContainer container) => 
            this.container = container;

        public void Enter<TState>() where TState : IState
        {
            activeState?.Exit();
            activeState = container.allStates[typeof(TState)];
            activeState.Enter();
        }

        public void Enter<TState>(GameCell clickedCell, GameCell activeCell)
        {
            activeState?.Exit();
            activeState = container.allStates[typeof(TState)];
            ((ISwapState)activeState).Enter(clickedCell, activeCell);
        }

        public void ActiveStateFinishedMove(GameCell gameCell)
        {
            if(activeState is IMovableState)
                ((IMovableState)activeState).FinishedMove(gameCell);
        }
    }
}