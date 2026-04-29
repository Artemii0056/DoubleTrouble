using Game.Combat;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public class ProjectileCollisionSystem
    {
        private readonly ProjectileHitSystem _hitSystem;
        private readonly LayerMask _targetMask;

        public ProjectileCollisionSystem(
            ProjectileHitSystem hitSystem)
        {
            _hitSystem = hitSystem;
            _targetMask = 1 << 6;
        }

        public void Tick(IProjectileSimulationService simulation, float deltaTime)
        {
            foreach (var projectile in simulation.Projectiles)
            {
                if (projectile.IsAlive == false)
                    continue;

                var distance = projectile.Speed * deltaTime;

                if (!Physics.SphereCast(
                        projectile.Position,
                        projectile.Radius,
                        projectile.Direction,
                        out RaycastHit hit,
                        distance,
                        _targetMask))
                {
                    continue;
                }

                if (!hit.collider.TryGetComponent(out IAimTarget target))
                    continue;

                _hitSystem.HandleHit(projectile, target);
            }
        }
    }
}