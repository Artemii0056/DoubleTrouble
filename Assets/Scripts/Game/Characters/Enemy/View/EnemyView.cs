using UnityEngine;

namespace Game.Characters.Enemy.View
{
    public sealed class EnemyView : MonoBehaviour
    {
        public int RuntimeId { get; private set; }

        public void Init(int runtimeId)
        {
            RuntimeId = runtimeId;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetDirection(Vector3 direction)
        {
            direction.y = 0f;

            if (direction.sqrMagnitude <= 0.001f)
                return;

            transform.forward = direction.normalized;
        }
    }
}