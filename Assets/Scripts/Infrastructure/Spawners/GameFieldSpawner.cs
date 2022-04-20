using Field;
using Infrastructure.Containers;
using Infrastructure.Misc;
using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.Spawners
{
    public class GameFieldSpawner
    {
        private readonly GameFieldContainer _fieldContainer;
        private readonly FieldCell.Factory _fieldCellFactory;
        private readonly GameFieldPositionCalculator _gameFieldPositionCalculator;
        private int length;
        private int height;



        public GameFieldSpawner(GameFieldContainer fieldContainer, FieldCell.Factory fieldCellFactory, GameFieldPositionCalculator gameFieldPositionCalculator)
        {
            length = GlobalCachedParameters.FieldLength;
            height = GlobalCachedParameters.FieldHeight;
            _fieldContainer = fieldContainer;
            _fieldCellFactory = fieldCellFactory;
            _gameFieldPositionCalculator = gameFieldPositionCalculator;
        }


        public void Spawn()
        {
            _fieldContainer.allFieldCells = new FieldCell[length, height];
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _fieldContainer.allFieldCells[i, j] = _fieldCellFactory.Create();
                    _fieldContainer.allFieldCells[i, j].SetCoordinates(i,j);
                    _gameFieldPositionCalculator.CalculateSize(length,height, _fieldContainer.allFieldCells[i, j].GetComponent<SpriteRenderer>().bounds.size.x);
                    _fieldContainer.allFieldCells[i, j].SetPosition(_gameFieldPositionCalculator.CalculatePosition(i,j));
                    _fieldContainer.allFieldCells[i, j].transform.localScale =
                        _gameFieldPositionCalculator.ReturnScale();
                }
            }
        }
    }
}