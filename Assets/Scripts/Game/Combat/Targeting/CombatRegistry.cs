using System.Collections.Generic;
using Game.Characters;
using UnityEngine;

namespace Game.Combat.Targeting
{
    public sealed class CombatRegistry
    {
        private readonly Dictionary<int, ITarget> _targets = new();
        private readonly Dictionary<int, IDamageable> _damageables = new();

        public void RegisterTarget(ITarget target)
        {
            _targets[target.Id] = target;
        }

        public void UnregisterTarget(int id)
        {
            _targets.Remove(id);
        }

        public bool TryGetTarget(int id, out ITarget target)
        {
            return _targets.TryGetValue(id, out target);
        }

        public ITarget GetClosestTarget(Vector3 position)
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

        public void RegisterDamageable(IDamageable damageable)
        {
            _damageables[damageable.Id] = damageable;
        }

        public void UnregisterDamageable(int id)
        {
            _damageables.Remove(id);
        }

        public bool TryGetDamageable(int id, out IDamageable damageable)
        {
            return _damageables.TryGetValue(id, out damageable);
        }
    }
}
