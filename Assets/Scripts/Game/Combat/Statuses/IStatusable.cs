namespace Game.Combat.Statuses
{
    public interface IStatusable
    {
        void ApplyStatus(StatusData status);
    }
}