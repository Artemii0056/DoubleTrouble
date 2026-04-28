using Game.Characters.Enemy.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyChaseService : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly TargetRuntimeRegistry _targetRegistry;

        public EnemyChaseService(
            EnemyRuntimeStorage enemyStorage,
            TargetRuntimeRegistry targetRegistry)
        {
            _enemyStorage = enemyStorage;
            _targetRegistry = targetRegistry;
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;

            foreach (var enemy in _enemyStorage.Enemies)
            {
                if (!enemy.IsAlive)
                    continue;

                ITargetRuntime target = GetOrFindTarget(enemy);

                if (target == null)
                    continue;

                Vector3 toTarget = target.Position - enemy.Position;
                toTarget.y = 0f;

                if (toTarget.sqrMagnitude <= 0.01f)
                    continue;

                enemy.Move(toTarget.normalized, deltaTime);
            }
        }

        private ITargetRuntime GetOrFindTarget(EnemyRuntime enemy)
        {
            if (enemy.TargetId.HasValue &&
                _targetRegistry.TryGet(enemy.TargetId.Value, out var currentTarget) &&
                currentTarget.IsAlive)
            {
                return currentTarget;
            }

            ITargetRuntime newTarget = _targetRegistry.GetClosest(enemy.Position);

            if (newTarget != null)
                enemy.SetTarget(newTarget.Id);

            return newTarget;
        }
    }
}