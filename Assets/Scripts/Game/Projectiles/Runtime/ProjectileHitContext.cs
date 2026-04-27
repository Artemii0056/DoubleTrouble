using Game.Combat;
using Game.Combat.Services;

namespace Game.Projectiles.Runtime
{
    public readonly struct ProjectileHitContext
    {
        public ProjectileHitContext(
            ProjectileRuntime projectile,
            ITargetable target,
            CombatServices services)
        {
            Projectile = projectile;
            Target = target;
            Services = services;
        }
    
        public readonly ITargetable Target;
        public readonly ProjectileRuntime Projectile;
        public readonly CombatServices Services;
    }
}