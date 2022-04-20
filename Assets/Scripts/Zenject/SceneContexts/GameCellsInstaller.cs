using GameCells;
using Infrastructure.Containers;
using Infrastructure.Services.GameCells;
using Infrastructure.Spawners;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class GameCellsInstaller : MonoInstaller
    {
        [SerializeField] private GameObject gameCellPrefab;
        [SerializeField] private GameObject gameCelConfigsPrefab;
        [SerializeField] private GameObject obstacleCellPrefab;

        public override void InstallBindings()
        {
            Container.Bind<GameCellsContainer>().AsSingle();
            Container.Bind<GameCellDynamicCleaner>().AsSingle();
            Container.Bind<GameCellsUpMover>().AsSingle();
            Container.Bind<GameCellsPositionSwapper>().AsSingle();
            Container.Bind<GameCellsClickProcessor>().AsSingle();
            Container.Bind<GameCellConfigsContainer>().FromComponentInNewPrefab(gameCelConfigsPrefab).AsSingle();
            Container.Bind<GameCellTypeChooser>().AsSingle();
            Container.Bind<GameCellsInitialSpawner>().AsSingle();
            Container.Bind<GameCellsRandomShuffler>().AsSingle();
            Container.BindFactory<GameCell, GameCell.Factory>().FromComponentInNewPrefab(gameCellPrefab);
            Container.BindFactory<ObstacleCell, ObstacleCell.Factory>().FromComponentInNewPrefab(obstacleCellPrefab);
            Container.Bind<GameCellCreator>().AsSingle();

        }
    }
}