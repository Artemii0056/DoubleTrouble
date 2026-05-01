using UnityEngine;

namespace Game.Characters.Enemy.View
{
    public sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Color _deadColor;

        private Color _aliveColor;
        private bool _hasAliveColor;

        public int RuntimeId { get; private set; }

        public void Init(int runtimeId)
        {
            CacheAliveColor();
            RuntimeId = runtimeId;
            _meshRenderer.material.color = _aliveColor;
        }

        public void Deactivate()
        {
            RuntimeId = 0;
            gameObject.SetActive(false);
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

        public void SetDeadVisual()
        {
            CacheAliveColor();
            _meshRenderer.material.color = _deadColor;
        }

        private void CacheAliveColor()
        {
            if (_hasAliveColor)
                return;

            _aliveColor = _meshRenderer.material.color;
            _hasAliveColor = true;
        }
    }
}
