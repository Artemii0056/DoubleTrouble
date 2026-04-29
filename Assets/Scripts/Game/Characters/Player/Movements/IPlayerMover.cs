using UnityEngine;

namespace Game.Characters.Player.Movements
{
    public interface IPlayerMover
    {
        void Init(CharacterController characterController);
        void Activate();
        void Deactivate();
    }
}
