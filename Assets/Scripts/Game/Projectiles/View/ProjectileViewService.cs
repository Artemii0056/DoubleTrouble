using System.Collections.Generic;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.View
{
    public sealed class ProjectileViewService : IProjectileViewService
    {
        private readonly Dictionary<int, ProjectileViewHandle> _views = new();
        private readonly Dictionary<ProjectileView, Stack<ProjectileView>> _pools = new();
        private readonly HashSet<int> _aliveIds = new();
        private readonly List<int> _toRelease = new();

        public void SpawnView(ProjectileRuntime runtime, ProjectileView prefab)
        {
            ProjectileView view = GetView(prefab);

            view.transform.SetPositionAndRotation(
                runtime.Position,
                Quaternion.LookRotation(runtime.Direction));

            view.Init(runtime);
            _views.Add(runtime.Id, new ProjectileViewHandle(view, prefab));
        }

        public void RenderAll(IEnumerable<ProjectileRuntime> projectiles)
        {
            _aliveIds.Clear();

            foreach (var projectile in projectiles)
            {
                _aliveIds.Add(projectile.Id);

                if (_views.TryGetValue(projectile.Id, out var handle))
                    handle.View.Render();
            }

            _toRelease.Clear();

            foreach (var pair in _views)
            {
                if (!_aliveIds.Contains(pair.Key))
                    _toRelease.Add(pair.Key);
            }

            foreach (int id in _toRelease)
                ReleaseView(id);
        }

        private ProjectileView GetView(ProjectileView prefab)
        {
            if (_pools.TryGetValue(prefab, out var pool) && pool.Count > 0)
                return pool.Pop();

            return Object.Instantiate(prefab);
        }

        private void ReleaseView(int id)
        {
            if (!_views.TryGetValue(id, out var handle))
                return;

            _views.Remove(id);
            handle.View.Deactivate();

            if (!_pools.TryGetValue(handle.Prefab, out var pool))
            {
                pool = new Stack<ProjectileView>();
                _pools.Add(handle.Prefab, pool);
            }

            pool.Push(handle.View);
        }

        private readonly struct ProjectileViewHandle
        {
            public ProjectileViewHandle(ProjectileView view, ProjectileView prefab)
            {
                View = view;
                Prefab = prefab;
            }

            public ProjectileView View { get; }
            public ProjectileView Prefab { get; }
        }
    }
}
