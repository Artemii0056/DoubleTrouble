using Game.Combat;
using Game.Combat.Targeting;
using UnityEngine;

namespace Game.Characters.Enemy
{
    public sealed class EnemyTarget : MonoBehaviour, ITargetable
    {
        [SerializeField] private Transform aimPoint;

        public int Id => gameObject.GetInstanceID();

        public Vector3 Position => transform.position;

        public Transform AimPoint => aimPoint != null ? aimPoint : transform;

        public bool IsAlive { get; private set; } = true;

        private TargetSearchService _targetSearchService;

        public void Construct(TargetSearchService targetSearchService)
        {
            _targetSearchService = targetSearchService;
        }

        private void OnEnable()
        {
            _targetSearchService?.Register(this);
        }

        private void OnDisable()
        {
            _targetSearchService?.Unregister(this);
        }

        public void Kill()
        {
            IsAlive = false;
            _targetSearchService?.Unregister(this);
        }
    }
}