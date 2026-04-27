using System.Collections.Generic;
using System.Linq;
using ShootSystem.Scripts;
using UnityEngine;

namespace TestSystem.TestProjectileLogic
{
    public sealed class TargetSearchService
    {
        private readonly List<ITargetable> _targets = new();

        public void Register(ITargetable target)
        {
            if (target == null)
                return;

            if (!_targets.Contains(target))
                _targets.Add(target);
        }

        public void Unregister(ITargetable target)
        {
            _targets.Remove(target);
        }

        public ITargetable FindNearestExcept(
            Vector3 position,
            int exceptTargetId,
            IReadOnlyCollection<int> ignoredTargetIds,
            float radius)
        {
            ITargetable nearest = null;
            var bestSqrDistance = radius * radius;

            for (int i = _targets.Count - 1; i >= 0; i--)
            {
                var target = _targets[i];

                if (target == null || !target.IsAlive || target.AimPoint == null)
                {
                    _targets.RemoveAt(i);
                    continue;
                }

                int targetId = GetTargetId(target);

                if (targetId == exceptTargetId)
                    continue;

                if (ignoredTargetIds.Contains(targetId))
                    continue;

                Vector3 targetPosition = target.AimPoint.position;
                float sqrDistance = (targetPosition - position).sqrMagnitude;

                if (sqrDistance > bestSqrDistance)
                    continue;

                bestSqrDistance = sqrDistance;
                nearest = target;
            }

            return nearest;
        }

        private static int GetTargetId(ITargetable target)
        {
            return target.AimPoint.GetInstanceID();
        }
    }
}