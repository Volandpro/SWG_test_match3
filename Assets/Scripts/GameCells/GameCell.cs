using System;
using Field;
using Infrastructure.Services;
using Infrastructure.Services.GameCells;
using Zenject;

namespace GameCells
{
    public class GameCell : BasicCell, IClickable
    {
        public Action OnSelect;
        public Action OnDestroy;
        public Action OnDeselect;
        public Action<FieldCell> OnMove;
        public bool isMoving;
        
        private GameCellsClickProcessor _clickProcessor;
        
        [Inject]
        public void Construct(GameCellsClickProcessor clickProcessor) => 
            this._clickProcessor = clickProcessor;
        

        public void Highlight() => OnSelect?.Invoke();

        public void DisableHighlight() => OnDeselect?.Invoke();

        public void TryMoveTo(FieldCell toCell)
        {
            isMoving = true;
            myFieldCell.TryClearGameCell(this);
            myFieldCell = toCell;
            myFieldCell.SetGameCell(this);
            OnMove?.Invoke(toCell);
        }

        public void Destroy()
        {
            OnDestroy?.Invoke();
            Destroy(this.gameObject);
        }
        public void Click() => _clickProcessor.WasClicked(this);

        public class Factory : PlaceholderFactory<GameCell>
        {
        }
    }
}