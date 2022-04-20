using System.Collections.Generic;
using Field;
using GameCells;
using Infrastructure.Containers;
using Infrastructure.Services;
using Infrastructure.Services.GameCells;

namespace Infrastructure.States.GameLoopStates
{
    public class AutoFillState : IMovableState
    {
        private readonly GameCellsUpMover _cellsUpMover;
        private readonly MatchChecker _matchChecker;
        private readonly GameCellsContainer _gameCellsContainer;
        private readonly GameLoopStateMachine _stateMachine;
        private readonly GameCellDynamicCleaner _cellDynamicCleaner;

        public AutoFillState(GameCellsUpMover cellsUpMover,MatchChecker matchChecker, GameCellsContainer gameCellsContainer, GameLoopStateMachine stateMachine,
            GameCellDynamicCleaner cellDynamicCleaner)
        {
            _cellsUpMover = cellsUpMover;
            _matchChecker = matchChecker;
            _gameCellsContainer = gameCellsContainer;
            _stateMachine = stateMachine;
            _cellDynamicCleaner = cellDynamicCleaner;
        }
        public void Enter()
        {
            MoveCellsUp();
        }

        private void MoveCellsUp()
        {
            _cellsUpMover.TryMoveCellsUp();
        }

        public void Exit()
        {
           
        }

        private bool HaveMovableCells()
        {
            for (int i = 0; i < _gameCellsContainer.allGameCells.Count; i++)
            {
                if (_gameCellsContainer.allGameCells[i]!=null && _gameCellsContainer.allGameCells[i].isMoving)
                {
                    return true;
                }
            }

            return false;
        }
        public void FinishedMove(GameCell gameCell)
        {
            List<FieldCell> matchedCells = _matchChecker.CheckForMatches(gameCell);
            if (matchedCells.Count>0)
            {
                _cellDynamicCleaner.ClearCells(matchedCells);
                MoveCellsUp();
            }

            if (!HaveMovableCells())
            {
                _stateMachine.Enter<RandomShuffleState>();
            }
        }
    }
}