using Infrastructure.Services;
using UnityEngine;

namespace Zenject.SceneContexts
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private GameObject inputServicePrefab;
        public override void InstallBindings()
        {
            InputService inputService = Container.InstantiatePrefabForComponent<InputService>(inputServicePrefab);
            Container.Bind<InputService>().FromInstance(inputService).AsSingle();
        }
    }
}