using System.Collections.Generic;
using Field;
using GameCells;
using Infrastructure.Containers;
using Zenject;

namespace Infrastructure.Services.GameCells
{
    public class GameCellDynamicCleaner
    {
        private readonly Score _score;
        private readonly GameCellsContainer _cellsContainer;

        [Inject]
        public GameCellDynamicCleaner(Score score,GameCellsContainer cellsContainer)
        {
            _score = score;
            _cellsContainer = cellsContainer;
        }

       public void ClearCells(List<FieldCell> matchedCells)
        {
            _score.AddScoreFromRowCount(matchedCells.Count);
            for (int i = 0; i < matchedCells.Count; i++)
            {
                _cellsContainer.allGameCells.Remove((GameCell)matchedCells[i].myGameCell);
                ((GameCell)matchedCells[i].myGameCell).Destroy();
                matchedCells[i].myGameCell = null;
            }
        }
    }
}