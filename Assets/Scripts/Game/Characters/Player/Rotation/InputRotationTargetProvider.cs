using Game.Input.InputReader;
using UnityEngine;

namespace Game.Characters.Player.Rotation
{
    public sealed class InputRotationTargetProvider : IRotationTargetProvider
    {
        private readonly IPlayerInputReader _inputReader;

        public InputRotationTargetProvider(IPlayerInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public bool TryGetRotation(out Vector3 direction)
        {
            Vector2 moveInput = _inputReader.MoveValue;

            direction = new Vector3(moveInput.x, 0f, moveInput.y);

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
