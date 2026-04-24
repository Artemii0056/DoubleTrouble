using Characters.Scripts;
using Input.InputReader.Scripts;
using Movements;
using Rotation;
using UnityEngine;
using Zenject;

namespace Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Character _character;
        
        private IPlayerInputReader _inputReader;
        private IPlayerMover _playerMover;
        private IPlayerRotator _playerRotator;

        [Inject]
        public void Init(IPlayerInputReader inputReader, IPlayerMover playerMover, IPlayerRotator playerRotator)
        {
            _inputReader = inputReader;
            _playerMover = playerMover;
            _playerRotator = playerRotator;

            
            _inputReader.OnEnable();
            
            _playerMover.Init(_character.transform, _character.CharacterController);
            _playerRotator.Init(_character.transform);
        }
    }
}