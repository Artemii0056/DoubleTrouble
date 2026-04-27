namespace Game.Core.StateMachine
{
    public interface IGameStateMachine : IStateMachine
    {
        void Tick();
    }
}