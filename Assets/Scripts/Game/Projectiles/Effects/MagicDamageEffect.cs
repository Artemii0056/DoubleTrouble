using Game.Combat.Damage;

namespace Game.Projectiles.Effects
{
    public sealed class MagicDamageEffect : IProjectileEffect
    {
        private readonly DamageService _damageService;
        private readonly float _damage;

        public MagicDamageEffect(float damage, DamageService damageService)
        {
            _damage = damage;
            _damageService = damageService;
        }

        public void OnHit(ProjectileHitContext context)
        {
            context.Damageable.TakeDamage(_damage);
        }
    }
}