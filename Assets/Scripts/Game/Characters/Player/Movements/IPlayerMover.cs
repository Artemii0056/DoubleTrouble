using UnityEngine;

namespace Game.Characters.Player.Movements
{
    public interface IPlayerMover
    {
        void Tick();
        void Init(Transform playerTransform, CharacterController characterController);
        void Activate();
        void Deactivate();
    }
}