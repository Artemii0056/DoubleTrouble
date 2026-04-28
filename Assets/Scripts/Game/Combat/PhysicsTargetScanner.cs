using Game.Characters.Enemy.Runtime;
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

        private TargetRuntimeStore _targetRuntimeStore;

        [Inject]
        public void Construct(TargetRuntimeStore targetRuntimeStore)
        {
            _targetRuntimeStore = targetRuntimeStore;
        }

        public IAimTargetable FindNearestTarget(Vector3 origin)
        {
            int count = Physics.OverlapSphereNonAlloc(
                origin,
                searchRadius,
                _hits,
                targetMask
            );

            IAimTargetable bestTarget = null;
            float bestDistanceSqr = float.MaxValue;

            for (int i = 0; i < count; i++)
            {
                if (!_hits[i].TryGetComponent(out IAimTargetable target))
                    continue;

                if (!_targetRuntimeStore.TryGet(target.Id, out var runtime))
                    continue;

                if (!runtime.IsAlive)
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