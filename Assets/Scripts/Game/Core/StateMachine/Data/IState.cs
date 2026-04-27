namespace Game.Core.StateMachine.Data
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}