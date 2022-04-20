using Infrastructure.Misc;
using UnityEngine;

namespace GameCells
{
    [RequireComponent(typeof(GameCell))]
    public class GameCellDestroy : Configable
    {
        private GameObject destroyEffect;
        private GameCellType cellType;
        private void Start()
        {
            this.GetComponent<GameCell>().OnDestroy += ShowDestroyEffect;
        }

        private void ShowDestroyEffect()
        {
            destroyEffect = Instantiate(cellType.destroyEffect, this.transform.position, Quaternion.identity);
            destroyEffect.transform.localScale = GlobalCachedParameters.CellScaleMod * Vector3.one;
            Destroy(destroyEffect,5);
        }
        public override void SetConfig(GameCellType cellType)
        {
            this.cellType = cellType;
        }
    }
}