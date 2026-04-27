using Game.Projectiles.Runtime;

namespace Game.Projectiles.Effects
{
    public interface IProjectileEffect
    {
        void OnHit(ProjectileHitContext context);
    }
}