using ShootSystem.Scripts;
using UnityEngine;

namespace TestSystem.TestProjectileLogic
{
    public class ProjectileCollisionService
    {
        private readonly ProjectileHitService _hitService;
        private readonly LayerMask _targetMask;

        public ProjectileCollisionService(
            ProjectileHitService hitService)
        {
            _hitService = hitService;
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

                if (!hit.collider.TryGetComponent(out ITargetable target))
                    continue;

                _hitService.HandleHit(projectile, target);
            }
        }
    }
}