using Infrastructure.Services;

namespace Zenject.SceneContexts
{
    public class MatchCheckerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<MatchChecker>().AsSingle();
        }
    }
}