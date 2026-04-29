namespace Game.Characters
{
    public interface IDamageable : ITarget
    {
        int Id { get; }
        bool IsAlive { get; }
        void TakeDamage(float damage);
    }
}