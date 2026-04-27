using System.Collections.Generic;
using TestSystem.TestProjectileLogic.Projectiles;

namespace TestSystem.TestProjectileLogic
{
    public interface IProjectileSimulationService
    {
        void Tick(float deltaTime);
        void Add(ProjectileRuntime projectile);
        IReadOnlyList<ProjectileRuntime> Projectiles { get; }
    }
}