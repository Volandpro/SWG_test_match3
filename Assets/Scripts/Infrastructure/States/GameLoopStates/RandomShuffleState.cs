using System.Collections.Generic;
using Field;
using Infrastructure.Services;
using Infrastructure.Services.GameCells;

namespace Infrastructure.States.GameLoopStates
{
    public class RandomShuffleState : IState
    {
        private readonly MatchChecker _matchChecker;
        private readonly GameLoopStateMachine _stateMachine;
        private readonly GameCellsRandomShuffler _shuffler;
        private readonly GameCellDynamicCleaner _cellDynamicCleaner;

        public RandomShuffleState(MatchChecker matchChecker,GameLoopStateMachine stateMachine, GameCellsRandomShuffler shuffler, GameCellDynamicCleaner cellDynamicCleaner)
        {
            _matchChecker = matchChecker;
            _stateMachine = stateMachine;
            _shuffler = shuffler;
            _cellDynamicCleaner = cellDynamicCleaner;
        }
        public void Enter()
        {
            CheckForMatch();
        }

        private void CheckForMatch()
        {
            List<FieldCell> firstMatchedRow = _matchChecker.CheckMatchesForAllCells(0);
            if (firstMatchedRow.Count > 0)
            {
                _cellDynamicCleaner.ClearCells(firstMatchedRow);
                _stateMachine.Enter<AutoFillState>();
                return;
            }
            if (_matchChecker.CheckMatchesForAllCells(1).Count>0)
            {
                _stateMachine.Enter<PlayingState>();
            }
            else
            {
                _shuffler.Shuffle(CheckForMatch);
            }
        }
        
        public void Exit()
        {
            
        }
    }
}