namespace Game.Combat.Targeting
{
    public interface ITargetHitHistory
    {
        bool Contains(int targetId);
    }
}
