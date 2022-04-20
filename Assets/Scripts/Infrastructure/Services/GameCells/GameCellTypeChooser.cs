using System.Collections.Generic;
using System.Linq;
using GameCells;
using Infrastructure.Containers;
using Infrastructure.Misc;
using UnityEngine;

namespace Infrastructure.Services.GameCells
{
    public class GameCellTypeChooser
    {
        private readonly GameCellConfigsContainer _configsContainer;
        private readonly GameFieldContainer _fieldContainer;

        public GameCellTypeChooser(GameCellConfigsContainer configsContainer,GameFieldContainer fieldContainer)
        {
            _configsContainer = configsContainer;
            _fieldContainer = fieldContainer;
        }
        public GameCellType CalculateType(int currentXCoord,int currentYCoord)
        {
            List<GameCellType> availableTypes = _configsContainer.allCellTypes.ToList();
            GameCellType excludedType = CheckPreviousHorizontalTypes(currentXCoord,currentYCoord);
            availableTypes.Remove(excludedType);
            excludedType = CheckPreviousVerticalTypes(currentXCoord,currentYCoord);
            availableTypes.Remove(excludedType);
            return availableTypes[Random.Range(0,availableTypes.Count)];
        }

        private GameCellType CheckPreviousVerticalTypes(int currentXCoord, int currentYCoord)
        {
            GameCellType returnedCellType = null;
            if (currentYCoord >= GlobalCachedParameters.NeededCountInRow-1)
            {
                if(_fieldContainer.allFieldCells[currentXCoord, currentYCoord-1].myGameCell!=null)
                    returnedCellType =  _fieldContainer.allFieldCells[currentXCoord, currentYCoord-1].myGameCell.cellType;
                for (int i = currentYCoord - 1; i > currentYCoord - GlobalCachedParameters.NeededCountInRow; i--)
                {
                    if (_fieldContainer.allFieldCells[currentXCoord, i].myGameCell != null)
                    {
                        if (returnedCellType != _fieldContainer.allFieldCells[currentXCoord, i].myGameCell.cellType)
                            return null;
                        else
                            returnedCellType = _fieldContainer.allFieldCells[currentXCoord, i].myGameCell.cellType;
                    }
                }
            }

            return returnedCellType;
        }

        public GameCellType ReturnObstacle()
        {
            return _configsContainer.obstacleCellType;
        }
        private GameCellType CheckPreviousHorizontalTypes(int currentXCoord, int currentYCoord)
        {
            GameCellType returnedCellType = null;
            if (currentXCoord >= GlobalCachedParameters.NeededCountInRow-1)
            {
                if(_fieldContainer.allFieldCells[currentXCoord-1, currentYCoord].myGameCell!=null)
                    returnedCellType =  _fieldContainer.allFieldCells[currentXCoord-1, currentYCoord].myGameCell.cellType;
                for (int i = currentXCoord - 1; i > currentXCoord - GlobalCachedParameters.NeededCountInRow; i--)
                {
                    if (_fieldContainer.allFieldCells[i, currentYCoord].myGameCell != null)
                    {
                        if (returnedCellType != _fieldContainer.allFieldCells[i, currentYCoord].myGameCell.cellType)
                            return null;
                        else
                            returnedCellType = _fieldContainer.allFieldCells[i, currentYCoord].myGameCell.cellType;
                    }
                }
            }

            return returnedCellType;
        }
    }
}