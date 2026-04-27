using TestSystem.TestProjectileLogic.Effects;
using TestSystem.TestProjectileLogic.Projectiles;
using UnityEngine;

namespace TestSystem.TestProjectileLogic
{
    public interface IProjectileSpawnService
    {
        ProjectileRuntime Spawn(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction);
    }
}