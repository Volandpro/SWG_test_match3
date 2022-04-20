using Field;
using GameCells;
using Infrastructure.Containers;

namespace Infrastructure.Services.GameCells
{
    public class GameCellsUpMover
    {
        private readonly GameFieldContainer _fieldContainer;
        private readonly GameCellCreator _cellCreator;

        public GameCellsUpMover(GameFieldContainer fieldContainer, GameCellCreator cellCreator)
        {
            _fieldContainer = fieldContainer;
            _cellCreator = cellCreator;
        }
        public void TryMoveCellsUp()
        {
            for (int i = 0; i < _fieldContainer.allFieldCells.GetLength(0); i++)
            {
                for (int j = _fieldContainer.allFieldCells.GetLength(1)-1; j >= 0; j--)
                {
                    if (_fieldContainer.allFieldCells[i, j].myGameCell == null)
                    {
                        GameCell cellToMoveUp = FindCellToMoveUp(_fieldContainer.allFieldCells[i, j]);
                        if(cellToMoveUp!=null)
                            cellToMoveUp.TryMoveTo(_fieldContainer.allFieldCells[i, j]);
                        else
                        {
                            cellToMoveUp= _cellCreator.CreateNewCell(_fieldContainer.allFieldCells[i, j]);
                            cellToMoveUp.TryMoveTo(_fieldContainer.allFieldCells[i, j]);
                        }
                    }
                }
            }
        }

        private GameCell FindCellToMoveUp(FieldCell currentCell)
        {
            for (int i = currentCell.yCoord - 1; i >= 0; i--)
            {
                if (_fieldContainer.allFieldCells[currentCell.xCoord, i].myGameCell != null&&
                    _fieldContainer.allFieldCells[currentCell.xCoord, i].myGameCell is GameCell)
                {
                    return (GameCell)_fieldContainer.allFieldCells[currentCell.xCoord, i].myGameCell;
                }
            }
            return null;
        }
    }
}