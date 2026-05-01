using Game.Combat.Targeting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Combat
{
    public sealed class PlayerCombatController : MonoBehaviour
    {
        [FormerlySerializedAs("weaponShooter")] [SerializeField] private WeaponShooter _weaponShooter;

        private PlayerTargetService _targetService;

        [Inject]
        public void Construct(PlayerTargetService targetService)
        {
            _targetService = targetService;
        }

        private void Update()
        {
            IAimTarget currentTarget = _targetService.CurrentTarget;

            if (currentTarget == null)
                return;

            _weaponShooter.TryShoot(currentTarget);
        }
    }
}
