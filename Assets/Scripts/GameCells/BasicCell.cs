using System.Collections.Generic;
using Field;
using Infrastructure.Misc;
using UnityEngine;

namespace GameCells
{
    public abstract class BasicCell : MonoBehaviour
    {
        public FieldCell myFieldCell;
        public GameCellType cellType;
        protected List<Configable> allConfigable = new List<Configable>();

        private void Start() => 
            transform.localScale = Vector3.one*GlobalCachedParameters.CellScaleMod;
        public void SetPosition(Vector3 newPosition) => transform.position = newPosition;
        public void AddConfibale(Configable newConfigable) => 
            allConfigable.Add(newConfigable);

        public void SetType(GameCellType cellType)
        {
            this.cellType = cellType;
            SetConfig(cellType);
        }

        private void SetConfig(GameCellType gameCellType)
        {
            for (int i = 0; i < allConfigable.Count; i++)
            {
                allConfigable[i].SetConfig(gameCellType);
            }
        }
    }
}