using Game.Characters.Enemy.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyChaseSystem : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly TargetRuntimeStore _targetStore;

        public EnemyChaseSystem(
            EnemyRuntimeStorage enemyStorage,
            TargetRuntimeStore targetStore)
        {
            _enemyStorage = enemyStorage;
            _targetStore = targetStore;
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;

            foreach (var enemy in _enemyStorage.Enemies)
            {
                if (!enemy.IsAlive)
                    continue;

                ITarget target = GetOrFindTarget(enemy);

                if (target == null)
                    continue;

                Vector3 toTarget = target.Position - enemy.Position;
                toTarget.y = 0;

                if (toTarget.sqrMagnitude <= 0.01f)
                    continue;

                enemy.Move(toTarget.normalized, deltaTime);
            }
        }

        private ITarget GetOrFindTarget(Runtime.Enemy enemy)
        {
            if (enemy.TargetId.HasValue &&
                _targetStore.TryGet(enemy.TargetId.Value, out var currentTarget) &&
                currentTarget.IsAlive)
            {
                return currentTarget;
            }

            ITarget newTarget = _targetStore.GetClosest(enemy.Position);

            if (newTarget != null)
                enemy.SetTarget(newTarget.Id);

            return newTarget;
        }
    }
}