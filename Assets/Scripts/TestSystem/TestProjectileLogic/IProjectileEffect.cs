using TestSystem.TestProjectileLogic.Projectiles;

namespace TestSystem.TestProjectileLogic
{
    public interface IProjectileEffect
    {
        void OnHit(ProjectileHitContext context);
    }
}