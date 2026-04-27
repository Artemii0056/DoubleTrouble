using Player.Scripts;
using ShootSystem.Scripts;
using UnityEngine;

namespace TestSystem
{
    public class TargetRotationProvider : IRotationTargetProvider
    {
        private readonly TargetScanner _scanner;
        private readonly Transform _player;

        public TargetRotationProvider(TargetScanner scanner, IPlayerTransform playerTransform)
        {
            _scanner = scanner;
            _player = playerTransform.Transform;
        }

        public bool TryGetRotation(out Vector3 direction)
        {
            var target = _scanner.FindNearestTarget(_player.position);

            if (target == null)
            {
                direction = default;
                return false;
            }

            direction = target.AimPoint.position - _player.position;
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