using UnityEngine;

namespace GameCells
{
    public class SpriteChanger : Configable
    {
        private SpriteRenderer renderer;
        private void GetRenderer() => 
            renderer = this.GetComponent<SpriteRenderer>();

        public override void SetConfig(GameCellType cellType)
        {
            if(renderer==null)
                GetRenderer();
            renderer.sprite = cellType.cellImage;
        }
    }
}