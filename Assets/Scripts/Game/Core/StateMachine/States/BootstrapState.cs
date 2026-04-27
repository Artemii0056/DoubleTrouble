using Game.Core.StateMachine.Data;

namespace Game.Core.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;

        public BootstrapState(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        public void Enter()
        {
            _gameStateMachine.Enter<LoadState>();
        }

        public void Exit()
        {
            
        }
    }
}