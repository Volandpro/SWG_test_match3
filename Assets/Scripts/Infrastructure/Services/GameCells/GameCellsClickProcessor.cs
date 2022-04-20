using GameCells;
using Infrastructure.States.GameLoopStates;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.GameCells
{
    public class GameCellsClickProcessor
    {
        private readonly GameLoopStateMachine _stateMachine;
        private GameCell activeCell;

        [Inject]
        public GameCellsClickProcessor(GameLoopStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
        public void WasClicked(GameCell clickedCell)
        {
            if(clickedCell==activeCell)
                return;
            
            if (activeCell == null)
            {
                activeCell = clickedCell;
                activeCell.Highlight();
            }
            else
            {
                activeCell.DisableHighlight();
                if (ClickCellIsNearActive(clickedCell))
                {
                    _stateMachine.Enter<SwapState>(clickedCell,activeCell);
                    activeCell = null;
                }
                else
                {
                    activeCell = clickedCell;
                    activeCell.Highlight();
                }
            }
            
        }

        private bool ClickCellIsNearActive(GameCell clickedCell)
        {
            if (Mathf.Abs(clickedCell.myFieldCell.xCoord - activeCell.myFieldCell.xCoord) +
                Mathf.Abs(clickedCell.myFieldCell.yCoord - activeCell.myFieldCell.yCoord) < 2)
                return true;
            else
                return false;
        }
    }
}