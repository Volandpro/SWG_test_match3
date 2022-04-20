using System;
using System.Collections.Generic;
using System.Linq;
using GameCells;
using Infrastructure.Containers;
using Random = UnityEngine.Random;

namespace Infrastructure.Services.GameCells
{
    public class GameCellsRandomShuffler
    {
        private readonly GameFieldContainer _fieldContainer;
        private readonly GameCellsContainer _cellsContainer;

        public Action OnShuffled;

        public GameCellsRandomShuffler(GameFieldContainer fieldContainer, GameCellsContainer cellsContainer)
        {
            _fieldContainer = fieldContainer;
            _cellsContainer = cellsContainer;
        }

        public void Shuffle(Action CheckForMatch)
        {
            List<GameCell> cellsToShuffle = _cellsContainer.allGameCells.ToList();
            for (int i = 0; i < _fieldContainer.allFieldCells.GetLength(0); i++)
            {
                for (int j = 0; j < _fieldContainer.allFieldCells.GetLength(1); j++)
                {
                    if (!(_fieldContainer.allFieldCells[i, j].myGameCell is ObstacleCell))
                    {
                        int randomIndex = Random.Range(0, cellsToShuffle.Count);
                        _fieldContainer.allFieldCells[i, j].SetGameCell(cellsToShuffle[randomIndex]);
                        cellsToShuffle[randomIndex].myFieldCell = _fieldContainer.allFieldCells[i, j];
                        cellsToShuffle[randomIndex].SetPosition(_fieldContainer.allFieldCells[i, j].transform.position);
                        cellsToShuffle.Remove(cellsToShuffle[randomIndex]);
                    }
                }
            }
            OnShuffled?.Invoke();
            CheckForMatch?.Invoke();
        }
        
    }
}