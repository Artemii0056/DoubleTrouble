using Game.Characters.Player.Movements;
using Game.Characters.Player.Rotation;
using Game.Characters.Player.Scripts;
using Game.Input.InputReader.Scripts;
using UnityEngine;
using Zenject;

namespace Game.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        
        private IPlayerInputReader _inputReader;
        private IPlayerMover _playerMover;

        [Inject]
        public void Init(IPlayerInputReader inputReader, IPlayerMover playerMover)
        {
            _inputReader = inputReader;
            _playerMover = playerMover;

            _inputReader.OnEnable();
            
            _playerMover.Init(_playerView.transform, _playerView.CharacterController);
        }
    }
}