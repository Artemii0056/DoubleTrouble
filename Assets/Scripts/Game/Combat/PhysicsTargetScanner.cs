using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.Services;
using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Combat
{
    public class PhysicsTargetScanner : MonoBehaviour
    {
        [SerializeField] private float searchRadius = 8f;
        [SerializeField] private LayerMask targetMask;

        private readonly Collider[] _hits = new Collider[32];

        private DamageableRuntimeStore _damageableStore;

        [Inject]
        public void Construct(DamageableRuntimeStore damageableStore)
        {
            _damageableStore = damageableStore;
        }

        public IAimTarget FindNearestTarget(Vector3 origin)
        {
            int count = Physics.OverlapSphereNonAlloc(
                origin,
                searchRadius,
                _hits,
                targetMask
            );

            IAimTarget bestTarget = null;
            float bestDistanceSqr = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                IAimTarget target = null;

                if (!_hits[i].TryGetComponent(out target))
                    target = _hits[i].GetComponentInParent<IAimTarget>();

                if (target == null || target.AimPoint == null)
                    continue;

                if (!_damageableStore.TryGet(target.Id, out var damageable))
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
            Gizmos.DrawWireSphere(transform.position, searchRadius);
        }
    }
}