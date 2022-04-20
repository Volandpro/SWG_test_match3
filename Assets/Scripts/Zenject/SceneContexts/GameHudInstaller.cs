using UnityEngine;

namespace Zenject.SceneContexts
{
    public class GameHudInstaller : MonoInstaller
    {
        [SerializeField] private GameObject hud;
        public override void InstallBindings()
        {
            Container.InstantiatePrefab(hud);
        }
    }
}