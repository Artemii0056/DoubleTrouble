using System.Collections.Generic;
using System.Linq;
using Game.Characters;
using Game.Characters.Enemy.Runtime;
using UnityEngine;

namespace Game.Combat.Targeting
{
    public sealed class TargetSelectionService
    {
        private readonly List<IAimTargetable> _targets = new();
        private readonly TargetRuntimeStore _runtimeStore;

        public TargetSelectionService(TargetRuntimeStore runtimeStore)
        {
            _runtimeStore = runtimeStore;
        }

        public void Register(IAimTargetable target)
        {
            if (target == null)
                return;

            if (!_targets.Contains(target))
                _targets.Add(target);
        }

        public void Unregister(IAimTargetable target)
        {
            _targets.Remove(target);
        }

        public IAimTargetable FindNearestExcept(
            Vector3 position,
            int exceptTargetId,
            IReadOnlyCollection<int> ignoredTargetIds,
            float radius)
        {
            IAimTargetable nearest = null;
            float bestSqrDistance = radius * radius;

            for (int i = _targets.Count - 1; i >= 0; i--)
            {
                IAimTargetable target = _targets[i];

                if (target == null || target.AimPoint == null)
                {
                    _targets.RemoveAt(i);
                    continue;
                }

                int targetId = target.Id;

                if (!_runtimeStore.TryGet(targetId, out ITarget runtime))
                    continue;

                if (!runtime.IsAlive)
                    continue;

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
    }
}