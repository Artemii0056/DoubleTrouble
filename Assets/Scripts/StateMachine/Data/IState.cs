namespace StateMachine.Data
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}