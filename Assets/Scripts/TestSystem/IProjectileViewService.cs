using System.Collections.Generic;
using TestSystem.TestProjectileLogic.Projectiles;

namespace TestSystem
{
    public interface IProjectileViewService
    {
        void SpawnView(ProjectileRuntime runtime, ProjectileView prefab);
        void RenderAll(IEnumerable<ProjectileRuntime> projectiles);
    }
}