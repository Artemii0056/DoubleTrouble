using Game.Combat.Damage;
using Game.Combat.Statuses;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.Effects
{
    public sealed class PhysicalDamageEffect : IProjectileEffect
    {
        private readonly DamageService _damageService;
        private readonly float _damage;

        public PhysicalDamageEffect(float damage, DamageService damageService)
        {
            _damage = damage;
            _damageService = damageService;
        }

        public void OnHit(ProjectileHitContext context)
        {
            _damageService.ApplyDamage(
                context.Target,
                new DamageData(
                    context.Projectile.OwnerId,
                    _damage,
                    DamageType.Physical
                )
            );
        }
    }
}