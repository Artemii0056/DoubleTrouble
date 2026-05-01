using System.Collections.Generic;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.Services
{
    public sealed class ProjectileSimulationService : IProjectileSimulationService
    {
        private readonly List<ProjectileRuntime> _projectiles;
        private readonly ProjectileRuntimePool _runtimePool;

        public ProjectileSimulationService(ProjectileRuntimePool runtimePool)
        {
            _runtimePool = runtimePool;
            _projectiles = new();
        }
        
        public IReadOnlyList<ProjectileRuntime> Projectiles => _projectiles;

        public void Tick(float deltaTime)
        {
            for (int i = _projectiles.Count - 1; i >= 0; i--)
            {
                var projectile = _projectiles[i];

                if (projectile.IsAlive == false)
                {
                    RemoveAt(i);
                    continue;
                }

                projectile.Tick(deltaTime);

                if (projectile.IsAlive == false)
                    RemoveAt(i);
            }
        }

        public void Add(ProjectileRuntime projectile)
        {
            _projectiles.Add(projectile);
        }

        private void RemoveAt(int index)
        {
            ProjectileRuntime projectile = _projectiles[index];
            _projectiles.RemoveAt(index);
            _runtimePool.Release(projectile);
        }
    }
}
