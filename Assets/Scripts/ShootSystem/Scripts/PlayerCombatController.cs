using UnityEngine;

namespace ShootSystem.Scripts
{
    public class PlayerCombatController : MonoBehaviour
    {
        [SerializeField] private TargetScanner targetScanner;
        [SerializeField] private WeaponShooter weaponShooter;
        

        private ITargetable _currentTarget;

        private void Update()
        {
            _currentTarget = targetScanner.FindNearestTarget(transform.position);

            Debug.Log(_currentTarget == null);
        
            if (_currentTarget == null)
                return;
        
            weaponShooter.TryShoot(_currentTarget);
        }
    }
}