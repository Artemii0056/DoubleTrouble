using System.Collections.Generic;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.View
{
    public interface IProjectileViewService
    {
        void SpawnView(ProjectileRuntime runtime, ProjectileView prefab);
        void RenderAll(IEnumerable<ProjectileRuntime> projectiles);
    }
}