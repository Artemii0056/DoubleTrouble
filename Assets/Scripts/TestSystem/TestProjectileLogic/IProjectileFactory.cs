using TestSystem.TestProjectileLogic.Effects;
using TestSystem.TestProjectileLogic.Projectiles;
using UnityEngine;

namespace TestSystem.TestProjectileLogic
{
    public interface IProjectileFactory
    {
        ProjectileRuntime Create(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction);
    }
}