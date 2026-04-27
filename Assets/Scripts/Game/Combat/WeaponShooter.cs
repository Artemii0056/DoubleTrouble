using TestSystem.TestProjectileLogic;
using TestSystem.TestProjectileLogic.Effects;
using TestSystem.TestProjectileLogic.Projectiles;
using UnityEngine;
using Zenject;

namespace ShootSystem.Scripts
{
    public class WeaponShooter : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig config;
        [SerializeField] private Transform firePoint;
        [SerializeField] private float fireRate = 3f;
        
        private IProjectileSpawnService _spawnService;

        [Inject]
        public void Init(IProjectileSpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        private float _nextShotTime;

        public void TryShoot(ITargetable target)
        {
            if (target == null)
                return;

            if (Time.time < _nextShotTime)
                return;

            _nextShotTime = Time.time + 1f / fireRate;

            Vector3 direction = (target.AimPoint.position - firePoint.position).normalized;

           ProjectileRuntime projectile =  _spawnService.Spawn(config,1, firePoint.position, direction);
        }
    }
}