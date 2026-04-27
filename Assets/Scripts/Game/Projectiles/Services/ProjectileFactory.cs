using System.Collections.Generic;
using Game.Combat.Services;
using Game.Projectiles.Effects;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public class ProjectileFactory : IProjectileFactory
    {
        private int _nextId = 1;

        private readonly CombatServices _combatServices;

        // public ProjectileFactory(CombatServices combatServices)
        // {
        //     _combatServices = combatServices;
        // }

        public ProjectileRuntime Create(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction)
        {
            List<IProjectileEffect> effects = new List<IProjectileEffect>();
            
            // foreach (var effectConfig in config.effects)
            // {
            //     //effects.Add(effectConfig.CreateEffect(_combatServices));
            // }
            
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
                effects);

            return projectile;
        }
    }
}