using Infrastructure.ActualProviders;
using Infrastructure.States.GameLoopStates;
using Zenject;

namespace Infrastructure.States.GameStates
{
    public class GameLoopState : IState
    {

        [Inject]
        public GameLoopState(ActualGameLoopStateMachineProvider gameLoopStatesProvider) => 
            gameLoopStatesProvider.OnStateMachineSet += SetGameLoopStateMachine;

        private void SetGameLoopStateMachine(GameLoopStateMachine gameLoopStateMachine) => 
            gameLoopStateMachine.Enter<LevelCreationState>();

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}