using UnityEngine;

namespace GameCells
{
    [RequireComponent(typeof(GameCell))]
    public class GameCellHighlighter : Configable
    {
        private GameObject highlightEffect;
        private GameCell gameCellRoot;
        private void Start()
        {
            gameCellRoot=this.GetComponent<GameCell>();
            gameCellRoot.OnSelect += EnableHighLight;
            gameCellRoot.OnDeselect += DisableHighLight;
        }

        private void OnDisable()
        {
            gameCellRoot.OnSelect -= EnableHighLight;
            gameCellRoot.OnDeselect -= DisableHighLight;
        }
        private void DisableHighLight() => highlightEffect.SetActive(false);

        private void EnableHighLight() => highlightEffect.SetActive(true);

        public override void SetConfig(GameCellType cellType)
        {
            highlightEffect = Instantiate(cellType.highlightEffect, this.transform.position, Quaternion.identity, this.transform);
            DisableHighLight();
        }
    }
}