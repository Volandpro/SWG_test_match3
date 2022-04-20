using System.Collections.Generic;
using Field;
using GameCells;
using Infrastructure.Services;
using Infrastructure.Services.GameCells;

namespace Infrastructure.States.GameLoopStates
{
    public class SwapState : ISwapState
    {
        private readonly GameCellsPositionSwapper _positionSwapper;
        private readonly MatchChecker _matchChecker;
        private readonly GameLoopStateMachine _stateMachine;
        private readonly InputService _inputService;
        private readonly GameCellDynamicCleaner _cellDynamicCleaner;
        private GameCell clickedCell;
        private GameCell activeCell;
        private int countFinishedMove;

        public SwapState(GameCellsPositionSwapper positionSwapper, MatchChecker matchChecker, GameLoopStateMachine stateMachine,InputService inputService,
            GameCellDynamicCleaner cellDynamicCleaner)
        {
            _positionSwapper = positionSwapper;
            _matchChecker = matchChecker;
            _stateMachine = stateMachine;
            _inputService = inputService;
            _cellDynamicCleaner = cellDynamicCleaner;
        }
        public void Enter()
        {
            
        }

        public void Enter(GameCell clickedCell, GameCell activeCell)
        {
            _inputService.enabled = false;
            countFinishedMove = 0;
            this.clickedCell = clickedCell;
            this.activeCell = activeCell;
            _positionSwapper.SwapPositions(clickedCell,activeCell);
        }

        public void Exit()
        {
            
        }

        public void FinishedMove(GameCell gameCell)
        {
            if (countFinishedMove ==1)
            {
                List<FieldCell>matchedCells = _matchChecker.CheckForMatches(clickedCell);
                foreach (FieldCell checkForMatch in _matchChecker.CheckForMatches(activeCell))
                    matchedCells.Add(checkForMatch);
                
                if (matchedCells.Count== 0)
                    _positionSwapper.SwapPositionsBack();
                else
                {
                    _cellDynamicCleaner.ClearCells(matchedCells);
                    _stateMachine.Enter<AutoFillState>();
                }
            }
            if(countFinishedMove>1)
                _stateMachine.Enter<PlayingState>();
            countFinishedMove++;

        }
    }
}