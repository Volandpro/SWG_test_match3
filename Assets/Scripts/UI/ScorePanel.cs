using Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI scoreLabel;
        
        [Inject]
        public void Construct(Score score)
        {
            score.OnScoreChanged += ChangeScoreLabel;
        }

        private void ChangeScoreLabel(int scoreValue)
        {
            scoreLabel.text = scoreValue.ToString();
        }
    }
}
