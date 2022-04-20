using Zenject;

namespace GameCells
{
    public class ObstacleCell : BasicCell
    {
        public class Factory : PlaceholderFactory<ObstacleCell>
        {
        }
    }
}