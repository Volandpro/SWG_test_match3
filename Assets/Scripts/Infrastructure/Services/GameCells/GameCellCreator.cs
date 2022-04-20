using Field;
using GameCells;
using Infrastructure.Containers;
using Infrastructure.Misc;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.GameCells
{
    public class GameCellCreator
    {
        private readonly GameCellsContainer _cellsContainer;
        private readonly GameFieldContainer _fieldContainer;
        private readonly GameCell.Factory _gameCellFactory;
        private readonly GameCellTypeChooser _typeChooser;

        [Inject]
        public GameCellCreator(GameCellsContainer cellsContainer,GameFieldContainer fieldContainer, GameCell.Factory gameCellFactory
            , GameCellTypeChooser typeChooser)
        {
            _cellsContainer = cellsContainer;
            _fieldContainer = fieldContainer;
            _gameCellFactory = gameCellFactory;
            _typeChooser = typeChooser;
        }
        public GameCell CreateNewCell(FieldCell fieldCell)
        {
            GameCell newGameCell = _gameCellFactory.Create();
            newGameCell.myFieldCell = fieldCell;
            newGameCell.SetPosition(CalculateNewPosition(fieldCell));
            newGameCell.SetType(_typeChooser.CalculateType(fieldCell.xCoord,fieldCell.yCoord));
            _cellsContainer.AddNewCell(newGameCell);
            return newGameCell;
        }

        private  Vector3 CalculateNewPosition(FieldCell fieldCell)
        {
            Vector3 newPosition = CalculateBottomPosition(fieldCell);
            if (fieldCell.yCoord + 1 < _fieldContainer.allFieldCells.GetLength(1))
            {
                Vector3 previousGameCellPosition =
                    _fieldContainer.allFieldCells[fieldCell.xCoord, fieldCell.yCoord + 1].myGameCell.transform.position;
                if (previousGameCellPosition.y <= newPosition.y)
                    newPosition = previousGameCellPosition - Vector3.up * GlobalCachedParameters.CellWidth *
                        GlobalCachedParameters.CellScaleMod;
            }

            return newPosition;
        }

        private Vector3 CalculateBottomPosition(FieldCell fieldCell)
        {
            return _fieldContainer.allFieldCells[fieldCell.xCoord, 0].transform.position - 
                   Vector3.up*GlobalCachedParameters.CellWidth*GlobalCachedParameters.CellScaleMod;
        }
    }
}