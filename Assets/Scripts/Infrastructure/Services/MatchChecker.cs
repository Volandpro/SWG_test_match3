using System;
using System.Collections.Generic;
using Field;
using GameCells;
using Infrastructure.Containers;
using Infrastructure.Misc;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class MatchChecker
    {
        private readonly GameFieldContainer _gameFieldContainer;

        [Inject]
        public MatchChecker(GameFieldContainer gameFieldContainer)
        {
            _gameFieldContainer = gameFieldContainer;
        }

        public List<FieldCell> CheckMatchesForAllCells(int offsetToCheck=0)
        {
            List<FieldCell> firstMatchedRow = new List<FieldCell>();
            for (int i = 0; i < _gameFieldContainer.allFieldCells.GetLength(0); i++)
            {
                for (int j = 0; j < _gameFieldContainer.allFieldCells.GetLength(1); j++)
                {
                    firstMatchedRow = CheckForMatches(_gameFieldContainer.allFieldCells[i, j].myGameCell, offsetToCheck);
                    if (firstMatchedRow.Count > 0)
                    {
                        return firstMatchedRow;
                    }
                }
            }

            return firstMatchedRow;
        }
        public List<FieldCell> CheckForMatches(BasicCell gameCellRoot, int offsetToCheck=0)
        {
            StructureContainer container = new StructureContainer();
            FindRecursiveCellsForMatch(gameCellRoot,container,offsetToCheck);

            return container.AddedFieldCells;
        }

        private List<FieldCell> FindRecursiveCellsForMatch(BasicCell gameCellRoot, StructureContainer container,
            int offsetToCheck)
        {
            List<FieldCell> allMatchedCells = new List<FieldCell>();
            CheckRecursiveHorizontal(gameCellRoot, container, offsetToCheck);

            allMatchedCells = CheckRecursiveVertical(gameCellRoot, container, offsetToCheck);
            RemoveMatchedFromContainer(container, allMatchedCells);
            return allMatchedCells;
        }

        private List<FieldCell> CheckRecursiveVertical(BasicCell gameCellRoot, StructureContainer container, int offsetToCheck)
        {
            List<FieldCell> allMatchedCells;
            allMatchedCells = CalculateVerticalMatch(gameCellRoot, offsetToCheck);
            allMatchedCells.Add(gameCellRoot.myFieldCell);

            if (allMatchedCells.Count > GlobalCachedParameters.NeededCountInRow - 1)
            {
                RemoveMatchedFromContainer(container, allMatchedCells);
                foreach (var allMatchedCell in allMatchedCells)
                    container.AddedFieldCells.Add(allMatchedCell);
                for (int i = 0; i < allMatchedCells.Count; i++)
                {
                    foreach (var fieldCell in FindRecursiveCellsForMatch(allMatchedCells[i].myGameCell, container, offsetToCheck))
                        allMatchedCells.Add(fieldCell);
                }
            }

            return allMatchedCells;
        }
        private void CheckRecursiveHorizontal(BasicCell gameCellRoot, StructureContainer container, int offsetToCheck)
        {
            List<FieldCell> allMatchedCells;
            allMatchedCells = CalculateHorizontalMatch(gameCellRoot, offsetToCheck);
            allMatchedCells.Add(gameCellRoot.myFieldCell);

            if (allMatchedCells.Count > GlobalCachedParameters.NeededCountInRow - 1)
            {
                RemoveMatchedFromContainer(container, allMatchedCells);
                foreach (var allMatchedCell in allMatchedCells)
                    container.AddedFieldCells.Add(allMatchedCell);
                for (int i = 0; i < allMatchedCells.Count; i++)
                {
                    foreach (var fieldCell in FindRecursiveCellsForMatch(allMatchedCells[i].myGameCell, container, offsetToCheck))
                        allMatchedCells.Add(fieldCell);
                }
            }
            allMatchedCells.Clear();
        }

        private static void RemoveMatchedFromContainer(StructureContainer container, List<FieldCell> allMatchedCells)
        {
            for (int i = 0; i < allMatchedCells.Count; i++)
            {
                if (container.AddedFieldCells.Contains(allMatchedCells[i]))
                {
                    allMatchedCells.Remove(allMatchedCells[i]);
                    i--;
                }
            }
        }

        private List<FieldCell> CalculateVerticalMatch(BasicCell currentFieldCell, int offsetToCheck)
        {
            List<FieldCell> returnedCells = new List<FieldCell>();
            CheckTop(currentFieldCell.myFieldCell, returnedCells,offsetToCheck);
            CheckBottom(currentFieldCell.myFieldCell, returnedCells,offsetToCheck);
            if(returnedCells.Count<GlobalCachedParameters.NeededCountInRow-1)
                returnedCells.Clear();
            return returnedCells;
        }

        private void CheckTop(FieldCell currentFieldCell, List<FieldCell> returnedCells, int offsetToCheck)
        {
            if (CheckForObstacleNear(currentFieldCell.xCoord, currentFieldCell.yCoord+1)) 
                return;
            for (int i = currentFieldCell.yCoord + 1+offsetToCheck; i < _gameFieldContainer.allFieldCells.GetLength(1); i++)
            {
                if (CheckToAdd(currentFieldCell, currentFieldCell.xCoord,i))
                {
                    returnedCells.Add(_gameFieldContainer.allFieldCells[currentFieldCell.xCoord, i]);
                }
                else break;
            }
        }

        private void CheckBottom(FieldCell currentFieldCell, List<FieldCell> returnedCells, int offsetToCheck)
        {
            if (CheckForObstacleNear(currentFieldCell.xCoord, currentFieldCell.yCoord-1)) 
                return;
            for (int i = currentFieldCell.yCoord - 1-offsetToCheck; i >= 0; i--)
            {
                if (CheckToAdd(currentFieldCell, currentFieldCell.xCoord,i))
                {
                    returnedCells.Add(_gameFieldContainer.allFieldCells[currentFieldCell.xCoord, i]);
                }
                else break;
            }
        }

        private List<FieldCell> CalculateHorizontalMatch(BasicCell currentFieldCell, int offsetToCheck)
        {
            List<FieldCell> returnedCells = new List<FieldCell>();
            CheckRight(currentFieldCell.myFieldCell, returnedCells,offsetToCheck);
            CheckLeft(currentFieldCell.myFieldCell, returnedCells,offsetToCheck);
            if(returnedCells.Count<GlobalCachedParameters.NeededCountInRow-1)
                returnedCells.Clear();
            return returnedCells;
        }

        private void CheckLeft(FieldCell currentFieldCell, List<FieldCell> returnedCells, int offsetToCheck)
        {
            if (CheckForObstacleNear(currentFieldCell.xCoord - 1, currentFieldCell.yCoord)) 
                return;
            for (int i = currentFieldCell.xCoord - 1-offsetToCheck; i >= 0; i--)
            {
                if (CheckToAdd(currentFieldCell, i,currentFieldCell.yCoord))
                {
                    returnedCells.Add(_gameFieldContainer.allFieldCells[i, currentFieldCell.yCoord]);
                }
                else break;
            }
        }

        private void CheckRight(FieldCell currentFieldCell, List<FieldCell> returnedCells, int offsetToCheck)
        {
            if (CheckForObstacleNear(currentFieldCell.xCoord + 1, currentFieldCell.yCoord)) 
                return;
            for (int i = currentFieldCell.xCoord + 1+offsetToCheck; i < _gameFieldContainer.allFieldCells.GetLength(0); i++)
            {
                if (CheckToAdd(currentFieldCell, i,currentFieldCell.yCoord))
                {
                    returnedCells.Add(_gameFieldContainer.allFieldCells[i, currentFieldCell.yCoord]);
                }
                else break;
            }
        }

        private bool CheckForObstacleNear(int xCoordToCheck, int yCoordToCheck)
        {
            if (xCoordToCheck >= 0 && xCoordToCheck < _gameFieldContainer.allFieldCells.GetLength(0) &&
                yCoordToCheck >= 0 && yCoordToCheck < _gameFieldContainer.allFieldCells.GetLength(1))
            {
                if (_gameFieldContainer.allFieldCells[xCoordToCheck, yCoordToCheck].myGameCell is
                    ObstacleCell)
                    return true;
            }

            return false;
        }

        private bool CheckIfVerticalNear(FieldCell currentFieldCell, int xCoord,int yCoord)
        {
            if (!(currentFieldCell.myGameCell is GameCell) || !(_gameFieldContainer.allFieldCells[xCoord, yCoord].myGameCell is GameCell))
                return false;
            
            if (((GameCell)currentFieldCell.myGameCell).isMoving)
                return false;

            float calculatedDistance =
                Vector3.up.y * GlobalCachedParameters.CellWidth * GlobalCachedParameters.CellScaleMod;
            float currentDistance = Mathf.Abs(currentFieldCell.myGameCell.transform.position.y -
                                              _gameFieldContainer.allFieldCells[xCoord, yCoord].myGameCell.transform
                                                  .position.y);
            bool isNear = Math.Abs(calculatedDistance * (currentFieldCell.yCoord - yCoord) - currentDistance) < 0.01f && 
                          currentFieldCell.yCoord!=yCoord && 
                          ((GameCell)_gameFieldContainer.allFieldCells[xCoord, yCoord].myGameCell).isMoving;

            return !((GameCell)currentFieldCell.myGameCell).isMoving &&
                   (!((GameCell)_gameFieldContainer.allFieldCells[xCoord, yCoord].myGameCell).isMoving || isNear);
        }

        private bool CheckEquility(FieldCell currentFieldCell, int xCoord,int yCoord)
        {
            bool equalTypes = _gameFieldContainer.allFieldCells[xCoord, yCoord].myGameCell != null &&
                          currentFieldCell.myGameCell != null &&
                          _gameFieldContainer.allFieldCells[xCoord, yCoord].myGameCell.cellType ==
                          currentFieldCell.myGameCell.cellType;
            return equalTypes;
        }
        private bool CheckToAdd(FieldCell currentFieldCell, int xCoord,int yCoord)
        {
            return CheckEquility(currentFieldCell, xCoord,yCoord) && CheckIfVerticalNear(currentFieldCell, xCoord,yCoord);
        }
    }
}