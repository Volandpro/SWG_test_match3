using Field;
using GameCells;

namespace Infrastructure.Services.GameCells
{
    public class GameCellsPositionSwapper
    {
        private GameCell firstCell;
        private GameCell secondCell;
        
        public void SwapPositions(GameCell firstCell,GameCell secondCell)
        {
            this.firstCell = firstCell;
            this.secondCell = secondCell;
            FieldCell rememberedFirstFieldCell = firstCell.myFieldCell;
            FieldCell rememberedSecondFieldCell = secondCell.myFieldCell;
            firstCell.TryMoveTo(rememberedSecondFieldCell);
            secondCell.TryMoveTo(rememberedFirstFieldCell);
        }

        public void SwapPositionsBack()
        {
            if (firstCell != null && secondCell != null)
            {
                SwapPositions(firstCell, secondCell);
                firstCell = null;
                secondCell = null;
            }
        }
    }
}