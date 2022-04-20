using GameCells;
using Infrastructure.Services.GameCells;
using UnityEngine;
using Zenject;

namespace Field
{
    public class FieldCell : MonoBehaviour
    {
        public int xCoord;
        public int yCoord;
        public BasicCell myGameCell;

        private GameCellsClickProcessor _clickProcessor;
        
        [Inject]
        public void Construct(GameCellsClickProcessor clickProcessor) => 
            this._clickProcessor = clickProcessor;
        public void SetPosition(Vector3 position) => transform.position = position;

        public void SetGameCell(BasicCell newGameCell)
        {
            myGameCell = newGameCell;
        }
        public void SetCoordinates(int i, int j)
        {
            xCoord = i;
            yCoord = j;
        }

        public void TryClearGameCell(GameCell gameCell)
        {
            if(myGameCell==gameCell)
                myGameCell = null;
        }

        public class Factory : PlaceholderFactory<FieldCell>
        {
        }
    }
}