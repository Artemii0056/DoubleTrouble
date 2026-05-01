using Game.Combat;
using Game.Combat.Targeting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Characters.Enemy
{
    public sealed class EnemyAimTarget : MonoBehaviour, IAimTarget
    {
        [FormerlySerializedAs("aimPoint")] [SerializeField] private Transform _aimPoint;

        private TargetSelectionService _targetSelectionService;

        public int Id { get; private set; }
        public Transform AimPoint => _aimPoint != null ? _aimPoint : transform;

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
