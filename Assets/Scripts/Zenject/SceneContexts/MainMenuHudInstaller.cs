using UnityEngine;

namespace Zenject.SceneContexts
{
    public class MainMenuHudInstaller : MonoInstaller
    {
        [SerializeField] private GameObject hud;
        public override void InstallBindings()
        {
            Container.InstantiatePrefab(hud);
        }
    }
}