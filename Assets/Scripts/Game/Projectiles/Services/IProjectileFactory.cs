using Game.Projectiles.Effects;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
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