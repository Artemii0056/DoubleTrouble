using System.Collections.Generic;
using UnityEngine;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class TargetRuntimeRegistry
    {
        private readonly Dictionary<int, ITargetRuntime> _targets = new();

        public void Register(ITargetRuntime target)
        {
            _targets[target.Id] = target;
        }

        public void Unregister(int id)
        {
            _targets.Remove(id);
        }

        public bool TryGet(int id, out ITargetRuntime target)
        {
            return _targets.TryGetValue(id, out target);
        }

        public ITargetRuntime GetClosest(Vector3 position)
        {
            ITargetRuntime closest = null;
            float bestSqrDistance = float.MaxValue;

            foreach (var target in _targets.Values)
            {
                if (!target.IsAlive)
                    continue;

                float sqrDistance = (target.Position - position).sqrMagnitude;

                if (sqrDistance < bestSqrDistance)
                {
                    bestSqrDistance = sqrDistance;
                    closest = target;
                }
            }

            return closest;
        }
    }
}