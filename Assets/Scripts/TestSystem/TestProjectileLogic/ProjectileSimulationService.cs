using System.Collections.Generic;
using TestSystem.TestProjectileLogic.Projectiles;

namespace TestSystem.TestProjectileLogic
{
    public sealed class ProjectileSimulationService : IProjectileSimulationService
    {
        private readonly List<ProjectileRuntime> _projectiles;

        public ProjectileSimulationService()
        {
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
                    _projectiles.RemoveAt(i);
                    continue;
                }

                projectile.Tick(deltaTime);

                if (projectile.IsAlive == false)
                    _projectiles.RemoveAt(i);
            }
        }

        public void Add(ProjectileRuntime projectile)
        {
            _projectiles.Add(projectile);
        }
    }
}