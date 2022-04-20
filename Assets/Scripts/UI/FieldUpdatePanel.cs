using System.Collections;
using Infrastructure.Services.GameCells;
using UnityEngine;
using Zenject;

namespace UI
{
    public class FieldUpdatePanel : MonoBehaviour
    {
        private const int TimeToShowLabel = 3;
        [SerializeField] private TMPro.TextMeshProUGUI announceLabel;
        
        [Inject]
        public void Construct(GameCellsRandomShuffler shuffler) => 
            shuffler.OnShuffled += ShowLabel;

        private void ShowLabel()
        {
            announceLabel.enabled = true;
            StopAllCoroutines();
            StartCoroutine(DisableLabelAfterPause());
        }

        private IEnumerator DisableLabelAfterPause()
        {
            yield return new WaitForSeconds(TimeToShowLabel);
            announceLabel.enabled = false;
        }
    }
}
