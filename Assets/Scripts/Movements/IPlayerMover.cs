using UnityEngine;

namespace Movements
{
    public interface IPlayerMover
    {
        void Tick();
        void Init(Transform playerTransform, CharacterController characterController);
        void Activate();
        void Deactivate();
    }
}