using Field;
using Infrastructure.Containers;
using Infrastructure.Services;
using Infrastructure.Spawners;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class FieldInstaller : MonoInstaller
    {
        [SerializeField] private GameObject fieldCellPrefab;
        public override void InstallBindings()
        {
            Container.Bind<GameFieldPositionCalculator>().AsSingle();
            Container.Bind<GameFieldContainer>().AsSingle();
            Container.Bind<GameFieldSpawner>().AsSingle();
            Container.BindFactory<FieldCell, FieldCell.Factory>().FromComponentInNewPrefab(fieldCellPrefab);
        }
    }
}