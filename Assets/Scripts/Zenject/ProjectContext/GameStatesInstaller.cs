using System;
using System.Collections.Generic;
using Infrastructure.States;
using Infrastructure.States.GameStates;

namespace Zenject.ProjectContext
{
    public class GameStatesInstaller : MonoInstaller
    {
        private Dictionary<Type, IState> allStates;
        public override void InstallBindings()
        {
            StatesContainer statesContainer = InstallStatesContainer();
            InstallGameStateMachine();
            InstallStates();

            statesContainer.allStates = allStates;
        }

        private StatesContainer InstallStatesContainer()
        {
            StatesContainer statesContainer = Container.Instantiate<StatesContainer>();
            Container.Bind<StatesContainer>().FromInstance(statesContainer).AsSingle();
            return statesContainer;
        }

        private void InstallGameStateMachine()
        {
            Container.Bind<GameStateMachine>().AsSingle();
        }

        private void InstallStates()
        {
            allStates = new Dictionary<Type, IState>()
            {
                [typeof(BootStrapState)] = Container.Instantiate<BootStrapState>(),
                [typeof(MainMenuState)] = Container.Instantiate<MainMenuState>(),
                [typeof(LoadingState)] = Container.Instantiate<LoadingState>(),
                [typeof(GameLoopState)] = Container.Instantiate<GameLoopState>()
            };
            Container.Bind<Dictionary<Type, IState>>().FromInstance(allStates).AsSingle();
        }
    }
}