using System.Collections.Generic;
using Game.Projectiles.Effects;
using UnityEngine;

namespace Game.Projectiles.Runtime
{
    public sealed class ProjectileRuntimePool
    {
        private readonly Stack<ProjectileRuntime> _pool = new();

        public ProjectileRuntime Spawn(
            int id,
            int ownerId,
            Vector3 position,
            Vector3 direction,
            float speed,
            float radius,
            float lifetime,
            int pierceLeft,
            int ricochetLeft,
            IReadOnlyList<IProjectileEffect> effects)
        {
            if (_pool.Count == 0)
            {
                return new ProjectileRuntime(
                    id,
                    ownerId,
                    position,
                    direction,
                    speed,
                    radius,
                    lifetime,
                    pierceLeft,
                    ricochetLeft,
                    effects);
            }

            ProjectileRuntime runtime = _pool.Pop();

            runtime.Init(
                id,
                ownerId,
                position,
                direction,
                speed,
                radius,
                lifetime,
                pierceLeft,
                ricochetLeft,
                effects);

            return runtime;
        }

        public void Release(ProjectileRuntime runtime)
        {
            _pool.Push(runtime);
        }
    }
}
