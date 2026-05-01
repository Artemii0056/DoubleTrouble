using Game.Combat.Targeting;
using UnityEngine;

namespace Game.Characters.Player.Rotation
{
    public class TargetRotationProvider : IRotationTargetProvider
    {
        private readonly PlayerTargetService _targetService;

        public TargetRotationProvider(PlayerTargetService targetService)
        {
            _targetService = targetService;
        }

        public bool TryGetRotation(out Vector3 direction)
        {
            var target = _targetService.CurrentTarget;

            if (target == null)
            {
                direction = default;
                return false;
            }

            direction = target.AimPoint.position - _targetService.PlayerPosition;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.001f)
            {
                direction = default;
                return false;
            }

            direction.Normalize();
            return true;
        }
    }
}
