using Game.Projectiles.Services;
using Game.Projectiles.View;
using UnityEngine;
using Zenject;

namespace Game.Core.Tick
{
    public sealed class CombatTickController : MonoBehaviour
    {
        private IProjectileSimulationService _projectileSimulation;
        private ProjectileCollisionSystem _projectileCollision;
        private IProjectileViewService _projectileViews;

        [Inject]
        public void Init(IProjectileSimulationService projectileSimulation, ProjectileCollisionSystem projectileCollision, IProjectileViewService projectileViews)
        {
            _projectileSimulation = projectileSimulation;
            _projectileCollision = projectileCollision;
            _projectileViews = projectileViews;
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            _projectileSimulation.Tick(deltaTime);
            _projectileCollision.Tick(_projectileSimulation, deltaTime);
            _projectileViews.RenderAll(_projectileSimulation.Projectiles);
        }
    }
}