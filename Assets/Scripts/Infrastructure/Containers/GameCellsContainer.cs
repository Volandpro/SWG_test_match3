using System.Collections.Generic;
using GameCells;

namespace Infrastructure.Containers
{
    public class GameCellsContainer
    {
        public List<GameCell> allGameCells = new List<GameCell>();

        public void AddNewCell(GameCell newGameCell) => allGameCells.Add(newGameCell);
    }
}