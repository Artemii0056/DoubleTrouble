using Game.Combat;
using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy
{
    public sealed class EnemyAimTarget : MonoBehaviour, IAimTargetable
    {
        [SerializeField] private Transform aimPoint;

        private TargetSelectionService _targetSelectionService;

        public int Id { get; private set; }

        public Transform AimPoint => aimPoint != null ? aimPoint : transform;

        [Inject]
        public void Construct(TargetSelectionService targetSelectionService)
        {
            _targetSelectionService = targetSelectionService;
        }

        public void Init(int id)
        {
            Id = id;
        }

        private void OnEnable()
        {
            _targetSelectionService?.Register(this);
        }

        private void OnDisable()
        {
            _targetSelectionService?.Unregister(this);
        }
    }
}