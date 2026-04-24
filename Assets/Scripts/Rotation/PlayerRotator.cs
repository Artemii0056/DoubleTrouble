using Input.InputReader.Scripts;
using Rotation._ProjectFiles.Player.Scripts.Movements.Configs;
using StaticData.Scripts;
using UnityEngine;
using Zenject;

namespace Rotation
{
    public class PlayerRotator : IPlayerRotator, ITickable
    {
        private readonly IPlayerInputReader _inputReader;
        
        private float _pitch;

        private Transform _playerRoot;

        public PlayerRotator(IPlayerInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public void Init(Transform playerRoot)
        {
            _playerRoot = playerRoot;
        }

        public void Tick()
        {
            Vector2 moveInput = _inputReader.MoveValue;

            Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

            if (moveDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

                _playerRoot.rotation = Quaternion.Lerp(
                    _playerRoot.rotation,
                    targetRotation,
                    10f * Time.deltaTime
                );
            }
        }
    }
}