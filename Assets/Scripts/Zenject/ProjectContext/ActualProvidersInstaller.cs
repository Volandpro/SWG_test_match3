using Infrastructure.ActualProviders;

namespace Zenject.ProjectContext
{
    public class ActualProvidersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ActualGameLoopStateMachineProvider>().AsSingle();
        }
    }
}