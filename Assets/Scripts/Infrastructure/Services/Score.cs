using System;
using Infrastructure.Misc;

namespace Infrastructure.Services
{
    public class Score
    {
        public Action<int> OnScoreChanged;
        public int currentScore;

        public void AddScoreFromRowCount(int elementsCount)
        {
            currentScore += GlobalCachedParameters.BaseScoreForRow+(elementsCount-GlobalCachedParameters.NeededCountInRow)*GlobalCachedParameters.ScoresForAdditionalCells;
            OnScoreChanged?.Invoke(currentScore);
        }
    }
}