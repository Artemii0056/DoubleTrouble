using Game.Characters.Player.Movements.Data;
using Game.Infrastructure.StaticData;
using Game.Input.InputReader.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Characters.Player.Movements
{
    public class PlayerMover : IPlayerMover, ITickable
    {
        private readonly IPlayerInputReader _inputReader;
        private CharacterController _characterController;

        private readonly float _moveSpeed;
        private readonly float _gravity;

        private float _verticalVelocity;
        private Transform _playerTransform;
        
        private bool _isActive;

        public PlayerMover(
            IPlayerInputReader inputReader,
            IStaticDataService staticDataService)
        {
            PlayerMovementConfig config = staticDataService.PlayerMovementConfig;

            _moveSpeed = config.MoveSpeed;
            _gravity = config.Gravity;
            
            _inputReader = inputReader;

            _isActive = true;
        }

        public void Init(Transform playerTransform, CharacterController characterController)
        {
            _characterController = characterController;

            _playerTransform = playerTransform;
        }

        public void Tick()
        {
            if (_isActive == false)
                return;

            Vector2 moveInput = _inputReader.MoveValue;

            Vector3 moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

            if (moveDirection.sqrMagnitude > 1f)
                moveDirection.Normalize();

            Vector3 horizontalVelocity = moveDirection * _moveSpeed;

            if (_characterController.isGrounded && _verticalVelocity < 0f)
                _verticalVelocity = -2f;

            _verticalVelocity += _gravity * Time.deltaTime;

            Vector3 velocity = horizontalVelocity;
            velocity.y = _verticalVelocity;

            _characterController.Move(velocity * Time.deltaTime);
        }

        public void Activate() => 
            _isActive = true;

        public void Deactivate() => 
            _isActive = false;
    }
}