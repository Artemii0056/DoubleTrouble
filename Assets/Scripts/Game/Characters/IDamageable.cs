namespace Game.Characters
{
    public interface IDamageable : ITarget
    {
        void TakeDamage(float damage);
    }
}