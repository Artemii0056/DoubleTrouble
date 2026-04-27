using Player.Scripts;
using Rotation;
using UnityEngine;
using Zenject;

namespace TestSystem
{
    public class PlayerRotationController : ITickable
    {
        private readonly IRotationTargetProvider _targetProvider;
        private readonly IRotationTargetProvider _inputProvider;
        private readonly Transform _playerRoot;

        public PlayerRotationController(
            TargetRotationProvider targetProvider,
            InputRotationTargetProvider inputProvider,
            IPlayerTransform playerTransform)
        {
            _targetProvider = targetProvider;
            _inputProvider = inputProvider;
            _playerRoot = playerTransform.Transform;
        }

        public void Tick()
        {
            Vector3 direction;

            if (!_targetProvider.TryGetRotation(out direction))
            {
                if (!_inputProvider.TryGetRotation(out direction))
                    return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);

            _playerRoot.rotation = Quaternion.Lerp(
                _playerRoot.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }
    }
}