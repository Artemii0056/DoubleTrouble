using Game.Combat;

namespace Game.Projectiles.Runtime
{
    public readonly struct ProjectileHitContext
    {
        public ProjectileHitContext(
            ProjectileRuntime projectile,
            IAimTargetable target)
        {
            Projectile = projectile;
            Target = target;
        }
    
        public readonly IAimTargetable Target;
        public readonly ProjectileRuntime Projectile;
    }
}