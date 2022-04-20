using Infrastructure.Services;

namespace Infrastructure.States.GameLoopStates
{
    public class PlayingState : IState
    {
        private readonly InputService _inputService;

        public PlayingState(InputService inputService)
        {
            _inputService = inputService;
        }
        public void Enter()
        {
            _inputService.enabled = true;
        }
        public void Exit()
        {
            
        }
    }
}