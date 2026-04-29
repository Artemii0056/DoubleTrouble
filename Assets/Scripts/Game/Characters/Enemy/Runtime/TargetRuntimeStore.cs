using System.Collections.Generic;
using Game.Characters;
using UnityEngine;

namespace Game.Combat.Targeting
{
    public sealed class TargetRuntimeStore
    {
        private readonly Dictionary<int, ITarget> _targets = new();

        public void Register(ITarget target)
        {
            _targets[target.Id] = target;
        }

        public void Unregister(int id)
        {
            _targets.Remove(id);
        }

        public bool TryGet(int id, out ITarget target)
        {
            return _targets.TryGetValue(id, out target);
        }

        public ITarget GetClosest(Vector3 position)
        {
            ITarget closest = null;
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