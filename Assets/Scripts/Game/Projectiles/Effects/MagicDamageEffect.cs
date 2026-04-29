namespace Game.Projectiles.Effects
{
    public sealed class MagicDamageEffect : IProjectileEffect
    {
        private readonly float _damage;

        public MagicDamageEffect(float damage)
        {
            _damage = damage;
        }

        public void OnHit(ProjectileHitContext context)
        {
            context.Damageable.TakeDamage(_damage);
        }
    }
}
