using Game.Characters.Player.Scripts;
using Game.Combat;
using UnityEngine;

namespace Game.Characters.Player.Rotation
{
    public class TargetRotationProvider : IRotationTargetProvider
    {
        private readonly PhysicsTargetScanner _scanner;
        private readonly Transform _player;

        public TargetRotationProvider(PhysicsTargetScanner scanner, IPlayerTransform playerTransform)
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