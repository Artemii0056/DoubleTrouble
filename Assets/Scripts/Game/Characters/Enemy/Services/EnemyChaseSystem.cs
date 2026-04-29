using Game.Characters;
using Game.Characters.Enemy.Runtime;
using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyChaseSystem : ITickable
    {
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly CombatRegistry _combatRegistry;

        public EnemyChaseSystem(
            EnemyRuntimeStore enemyStore,
            CombatRegistry combatRegistry)
        {
            _enemyStore = enemyStore;
            _combatRegistry = combatRegistry;
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;

            foreach (var enemy in _enemyStore.Enemies)
            {
                if (!enemy.IsAlive)
                    continue;

                ITarget target = GetOrFindTarget(enemy);

                if (target == null)
                    continue;

                Vector3 toTarget = target.Position - enemy.Position;
                toTarget.y = 0f;

                if (toTarget.sqrMagnitude <= 0.01f)
                    continue;

                enemy.Move(toTarget.normalized, deltaTime);
            }
        }

        private ITarget GetOrFindTarget(EnemyRuntime enemy)
        {
            if (enemy.TargetId.HasValue &&
                _combatRegistry.TryGetTarget(enemy.TargetId.Value, out var currentTarget) &&
                currentTarget.IsAlive)
            {
                return currentTarget;
            }

            ITarget newTarget = _combatRegistry.GetClosestTarget(enemy.Position);

            if (newTarget != null)
                enemy.SetTarget(newTarget.Id);

            return newTarget;
        }
    }
}
