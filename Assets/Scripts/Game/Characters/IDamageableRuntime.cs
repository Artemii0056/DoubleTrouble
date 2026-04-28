namespace Game.Characters
{
    public interface IDamageableRuntime : ITargetRuntime
    {
        void TakeDamage(float damage);
    }
}