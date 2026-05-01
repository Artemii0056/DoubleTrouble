using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        [SerializeField] private WeaponShooter weaponShooter;

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

            weaponShooter.TryShoot(currentTarget);
        }
    }
}
