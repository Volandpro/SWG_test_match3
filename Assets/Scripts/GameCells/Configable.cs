using UnityEngine;

namespace GameCells
{
    [RequireComponent(typeof(BasicCell))]
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class Configable : MonoBehaviour
    {
        private void Awake()
        {
            this.GetComponent<BasicCell>().AddConfibale(this);
        }

        public virtual void SetConfig(GameCellType cellType)
        {
           
        }
    }
}