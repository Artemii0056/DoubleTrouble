using UnityEngine;

namespace Game.Characters.Player.View
{
    public sealed class PlayerView : MonoBehaviour, IPlayerTransform
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        
        public Transform Transform => transform;
        public Vector3 Position { get; }
        public bool IsAlive { get; }
    }
}
