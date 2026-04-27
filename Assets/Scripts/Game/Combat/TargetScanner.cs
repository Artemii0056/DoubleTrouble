using UnityEngine;

namespace Game.Combat
{
    public class TargetScanner : MonoBehaviour
    {
        [SerializeField] private float searchRadius = 8f;
        [SerializeField] private LayerMask targetMask;

        private readonly Collider[] _hits = new Collider[32];

        public ITargetable FindNearestTarget(Vector3 origin)
        {
            int count = Physics.OverlapSphereNonAlloc(
                origin,
                searchRadius,
                _hits,
                targetMask
            );

            ITargetable bestTarget = null;
            float bestDistanceSqr = float.MaxValue;
        
            for (int i = 0; i < count; i++)
            {
                if (!_hits[i].TryGetComponent(out ITargetable target))
                    continue;

                if (!target.IsAlive)
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