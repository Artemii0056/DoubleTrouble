using Game.Characters;
using Game.Combat;
using Game.Combat.Statuses;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.Effects
{
    public sealed class ProjectileHitContext
    {
        public ProjectileHitContext(
            ProjectileRuntime projectile,
            IAimTarget target,
            IDamageable damageable,
            IStatusable statusable)
        {
            Projectile = projectile;
            Target = target;
            Damageable = damageable;
            Statusable = statusable;
        }

        public ProjectileRuntime Projectile { get; }
        public IAimTarget Target { get; }
        public IDamageable Damageable { get; }
        public IStatusable Statusable { get; }
    }
}