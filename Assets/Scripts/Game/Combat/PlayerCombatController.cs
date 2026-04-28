using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Combat
{
    public class PlayerCombatController : MonoBehaviour
    {
        [FormerlySerializedAs("targetScanner")] [SerializeField] private PhysicsTargetScanner physicsTargetScanner;
        [SerializeField] private WeaponShooter weaponShooter;
        

        private IAimTargetable _currentTarget;

        private void Update()
        {
            _currentTarget = physicsTargetScanner.FindNearestTarget(transform.position);

            if (_currentTarget == null)
                return;
        
            weaponShooter.TryShoot(_currentTarget);
        }
    }
}