using Game.Combat.Targeting;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Combat
{
    public sealed class PhysicsTargetScanner : MonoBehaviour
    {
        [FormerlySerializedAs("searchRadius")] [SerializeField] private float _searchRadius = 8f;
        [FormerlySerializedAs("targetMask")] [SerializeField] private LayerMask _targetMask;

        private readonly Collider[] _hits = new Collider[32];

        private CombatRegistry _combatRegistry;

        [Inject]
        public void Construct(CombatRegistry combatRegistry)
        {
            _combatRegistry = combatRegistry;
        }

        public IAimTarget FindNearestTarget(Vector3 origin)
        {
            int count = Physics.OverlapSphereNonAlloc(
                origin,
                _searchRadius,
                _hits,
                _targetMask);

            IAimTarget bestTarget = null;
            float bestDistanceSqr = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                IAimTarget target = null;

                if (!_hits[i].TryGetComponent(out target))
                    target = _hits[i].GetComponentInParent<IAimTarget>();

                if (target == null || target.AimPoint == null)
                    continue;

                if (!_combatRegistry.TryGetDamageable(target.Id, out var damageable))
                    continue;

                if (!damageable.IsAlive)
                    continue;

                float distanceSqr = (target.AimPoint.position - origin).sqrMagnitude;

                if (distanceSqr < bestDistanceSqr)
                {
                    bestDistanceSqr = distanceSqr;
                    bestTarget = target;
                }
            }

            return bestTarget;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _searchRadius);
        }
    }
}
