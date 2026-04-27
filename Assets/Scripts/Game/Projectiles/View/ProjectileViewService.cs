using System.Collections.Generic;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.View
{
    public sealed class ProjectileViewService : IProjectileViewService
    {
        private readonly Dictionary<int, ProjectileView> _views = new();

        public void SpawnView(ProjectileRuntime runtime, ProjectileView prefab)
        {
            var view = Object.Instantiate(
                prefab,
                runtime.Position,
                Quaternion.LookRotation(runtime.Direction)
            );

            view.Init(runtime);
            _views.Add(runtime.Id, view);
        }

        public void RenderAll(IEnumerable<ProjectileRuntime> projectiles)
        {
            var aliveIds = new HashSet<int>();

            foreach (var projectile in projectiles)
            {
                aliveIds.Add(projectile.Id);

                if (_views.TryGetValue(projectile.Id, out var view))
                    view.Render();
            }

            var toRemove = new List<int>();

            foreach (var pair in _views)
            {
                if (!aliveIds.Contains(pair.Key))
                {
                    Object.Destroy(pair.Value.gameObject);
                    toRemove.Add(pair.Key);
                }
            }

            foreach (var id in toRemove)
                _views.Remove(id);
        }
    }
}