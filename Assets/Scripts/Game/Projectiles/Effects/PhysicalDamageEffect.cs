using Game.Combat.Statuses;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.Effects
{
    public sealed class PhysicalDamageEffect : IProjectileEffect
    {
        private readonly float _damage;

        public PhysicalDamageEffect(float damage)
        {
            _damage = damage;
        }

        public void OnHit(ProjectileHitContext context)
        {
            context.Services.Damage.ApplyDamage(
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