using TestSystem.TestProjectileLogic.Effects;
using TestSystem.TestProjectileLogic.Projectiles;
using UnityEngine;

namespace TestSystem.TestProjectileLogic
{
    public sealed class ProjectileSpawnService : IProjectileSpawnService
    {
        private readonly IProjectileFactory _factory;
        private readonly IProjectileSimulationService _simulation;
        private readonly IProjectileViewService _views;

        public ProjectileSpawnService(
            IProjectileFactory factory,
            IProjectileSimulationService simulation,
            IProjectileViewService views)
        {
            _factory = factory;
            _simulation = simulation;
            _views = views;
        }

        public ProjectileRuntime Spawn(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction)
        {
            var projectile = _factory.Create(
                config,
                ownerId,
                position,
                direction
            );

            _simulation.Add(projectile);
            _views.SpawnView(projectile, config.Prefab);

            return projectile;
        }
    }
}