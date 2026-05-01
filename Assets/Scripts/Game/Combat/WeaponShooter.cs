using Game.Projectiles.Effects;
using Game.Projectiles.Services;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using PlayerRuntime = Game.Characters.Player.Runtime.Player;

namespace Game.Combat
{
    public sealed class WeaponShooter : MonoBehaviour
    {
        [FormerlySerializedAs("config")] [SerializeField] private ProjectileConfig _config;
        [FormerlySerializedAs("firePoint")] [SerializeField] private Transform _firePoint;
        [FormerlySerializedAs("fireRate")] [SerializeField] private float _fireRate = 3f;

        private IProjectileSpawnService _spawnService;
        private PlayerRuntime _owner;
        private float _nextShotTime;

        [Inject]
        public void Init(IProjectileSpawnService spawnService, PlayerRuntime owner)
        {
            _spawnService = spawnService;
            _owner = owner;
        }

        public void TryShoot(IAimTarget target)
        {
            if (target == null)
                return;

            if (Time.time < _nextShotTime)
                return;

            _nextShotTime = Time.time + 1f / _fireRate;

            Vector3 direction = (target.AimPoint.position - _firePoint.position).normalized;

            _spawnService.Spawn(_config, _owner.Id, _firePoint.position, direction);
        }
    }
}
