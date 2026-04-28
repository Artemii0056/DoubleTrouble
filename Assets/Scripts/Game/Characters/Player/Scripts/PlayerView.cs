using UnityEngine;

namespace Game.Characters.Player.Scripts
{
    public class PlayerView : MonoBehaviour, IPlayerTransform, IEnemyTarget
    {
        [field: SerializeField] public CharacterController CharacterController { get; private set; }
        
        public Transform Transform => transform;
        public Vector3 Position { get; }
        public bool IsAlive { get; }
        public void TakeDamage(float damage)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IEnemyTarget
    {
        Transform Transform { get; }
        Vector3 Position { get; }
        bool IsAlive { get; }
        void TakeDamage(float damage);
    }
}