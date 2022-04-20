using System.Collections.Generic;
using Field;
using GameCells;
using Infrastructure.Containers;
using Infrastructure.Misc;
using Infrastructure.Services.GameCells;
using Zenject;
using Random = UnityEngine.Random;

namespace Infrastructure.Spawners
{
    public class GameCellsInitialSpawner
    {
        private readonly GameCellsContainer _cellsContainer;
        private readonly GameFieldContainer _fieldContainer;
        private readonly GameCell.Factory _gameCellFactory;
        private readonly GameCellTypeChooser _typeChooser;
        private readonly ObstacleCell.Factory _obstacleFactory;


        [Inject]
        public GameCellsInitialSpawner(GameCellsContainer cellsContainer,GameFieldContainer fieldContainer, GameCell.Factory gameCellFactory
            , GameCellTypeChooser typeChooser, ObstacleCell.Factory obstacleFactory)
        {
            _cellsContainer = cellsContainer;
            _fieldContainer = fieldContainer;
            _gameCellFactory = gameCellFactory;
            _typeChooser = typeChooser;
            _obstacleFactory = obstacleFactory;
        }

        public void InitialSpawn()
        {
            SpawnObstacles();
            SpawnGameCells();
        }

        private void SpawnObstacles()
        {
            List<FieldCell> usedFieldCells = new List<FieldCell>();
            for (int i = 0; i < GlobalCachedParameters.CountObstacles; i++)
            {
                int randomX = Random.Range(0, _fieldContainer.allFieldCells.GetLength(0));
                int randomY = Random.Range(0, _fieldContainer.allFieldCells.GetLength(1));
                if (usedFieldCells.Contains(_fieldContainer.allFieldCells[randomX, randomY]))
                {
                    i--;
                    continue;
                }
                ObstacleCell newObstacleCell = _obstacleFactory.Create();
                newObstacleCell.myFieldCell = _fieldContainer.allFieldCells[randomX, randomY];
                newObstacleCell.SetPosition(_fieldContainer.allFieldCells[randomX, randomY].transform.position);
                newObstacleCell.SetType(_typeChooser.ReturnObstacle());
                _fieldContainer.allFieldCells[randomX, randomY].SetGameCell(newObstacleCell);
                usedFieldCells.Add(_fieldContainer.allFieldCells[randomX, randomY]);
            }
        }

        private void SpawnGameCells()
        {
            for (int i = 0; i < _fieldContainer.allFieldCells.GetLength(0); i++)
            {
                for (int j = 0; j < _fieldContainer.allFieldCells.GetLength(1); j++)
                {
                    if (_fieldContainer.allFieldCells[i, j].myGameCell == null)
                    {
                        GameCell newGameCell = _gameCellFactory.Create();
                        newGameCell.myFieldCell = _fieldContainer.allFieldCells[i, j];
                        newGameCell.SetPosition(_fieldContainer.allFieldCells[i, j].transform.position);
                        newGameCell.SetType(_typeChooser.CalculateType(i, j));
                        _cellsContainer.AddNewCell(newGameCell);
                        _fieldContainer.allFieldCells[i, j].SetGameCell(newGameCell);
                    }
                }
            }
        }
    }
}