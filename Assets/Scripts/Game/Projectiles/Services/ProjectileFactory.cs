using System.Collections.Generic;
using Game.Projectiles.Effects;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public class ProjectileFactory : IProjectileFactory
    {
        private int _nextId = 1;

        public ProjectileRuntime Create(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction)
        {
            List<IProjectileEffect> effects = new List<IProjectileEffect>();
            
            var projectile = new ProjectileRuntime(
                _nextId++,
                 ownerId,
                position,
                direction.normalized,
                config.speed,
                config.radius,
                config.lifetime,
                config.pierce,
                config.ricochet,
                effects, config.damage);

            return projectile;
        }
    }
}