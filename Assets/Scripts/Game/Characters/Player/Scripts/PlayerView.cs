using UnityEngine;

namespace Game.Characters.Player.Scripts
{
    public class PlayerView : MonoBehaviour, IPlayerTransform
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        
        public Transform Transform => transform;
    }
}