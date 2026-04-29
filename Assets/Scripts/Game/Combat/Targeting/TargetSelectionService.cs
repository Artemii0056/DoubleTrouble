using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat.Targeting
{
    public sealed class TargetSelectionService
    {
        private readonly List<IAimTarget> _targets = new();
        private readonly CombatRegistry _combatRegistry;

        public TargetSelectionService(CombatRegistry combatRegistry)
        {
            _combatRegistry = combatRegistry;
        }

        public void Register(IAimTarget target)
        {
            if (target == null)
                return;

            if (!_targets.Contains(target))
                _targets.Add(target);
        }

        public void Unregister(IAimTarget target)
        {
            _targets.Remove(target);
        }

        public IAimTarget FindNearestExcept(
            Vector3 position,
            int exceptTargetId,
            IReadOnlyCollection<int> ignoredTargetIds,
            float radius)
        {
            IAimTarget nearest = null;
            float bestSqrDistance = radius * radius;

            for (int i = _targets.Count - 1; i >= 0; i--)
            {
                IAimTarget target = _targets[i];

                if (target == null || target.AimPoint == null)
                {
                    _targets.RemoveAt(i);
                    continue;
                }

                int targetId = target.Id;

                if (!_combatRegistry.TryGetDamageable(targetId, out var damageable))
                    continue;

                if (!damageable.IsAlive)
                    continue;

                if (targetId == exceptTargetId)
                    continue;

                if (Contains(ignoredTargetIds, targetId))
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

        private static bool Contains(IReadOnlyCollection<int> ids, int targetId)
        {
            foreach (int id in ids)
            {
                if (id == targetId)
                    return true;
            }

            return false;
        }
    }
}
