using Infrastructure.Spawners;
using Zenject;

namespace Infrastructure.States.GameLoopStates
{
    public class LevelCreationState : IState
    {
        private readonly GameFieldSpawner _gameFieldSpawner;
        private readonly GameCellsInitialSpawner _cellsInitialSpawner;
        private readonly GameLoopStateMachine _stateMachine;

        [Inject]
        public LevelCreationState(GameFieldSpawner gameFieldSpawner, GameCellsInitialSpawner cellsInitialSpawner, GameLoopStateMachine stateMachine)
        {
            _gameFieldSpawner = gameFieldSpawner;
            _cellsInitialSpawner = cellsInitialSpawner;
            _stateMachine = stateMachine;
        }
        public void Enter()
        {
            _gameFieldSpawner.Spawn();
            _cellsInitialSpawner.InitialSpawn();
            _stateMachine.Enter<RandomShuffleState>();
        }
        
        public void Exit()
        {
        }
    }
}