using Game.Characters.Player.Scripts;
using Game.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace Game.Characters.Player.Rotation
{
    public class PlayerRotationController : ITickable
    {
        private readonly IRotationTargetProvider _targetProvider;
        private readonly IRotationTargetProvider _inputProvider;
        private readonly Transform _playerRoot;
        private readonly float _rotationSpeed;

        public PlayerRotationController(
            TargetRotationProvider targetProvider,
            InputRotationTargetProvider inputProvider,
            IPlayerTransform playerTransform,
            IStaticDataService staticData)
        {
            _targetProvider = targetProvider;
            _inputProvider = inputProvider;
            _playerRoot = playerTransform.Transform;
            
            _rotationSpeed = staticData.PlayerRotationConfig.RotationSpeed;
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
                _rotationSpeed * Time.deltaTime
            );
        }
    }
}