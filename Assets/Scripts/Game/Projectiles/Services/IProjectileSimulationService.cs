using System.Collections.Generic;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.Services
{
    public interface IProjectileSimulationService
    {
        void Tick(float deltaTime);
        void Add(ProjectileRuntime projectile);
        IReadOnlyList<ProjectileRuntime> Projectiles { get; }
    }
}