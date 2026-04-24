using UnityEngine;

namespace Characters.Scripts
{
    public class Character : MonoBehaviour
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
    }
}