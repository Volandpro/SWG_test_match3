using Infrastructure.Services;

namespace Zenject.SceneContexts
{
    public class ScoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Score>().AsSingle();
        }
    }
}