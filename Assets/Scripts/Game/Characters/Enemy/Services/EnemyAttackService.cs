using Game.Characters.Enemy.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyAttackService : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly TargetRuntimeRegistry _targetRegistry;
        private readonly DamageableRuntimeRegistry _damageableRegistry;

        public EnemyAttackService(
            EnemyRuntimeStorage enemyStorage,
            TargetRuntimeRegistry targetRegistry,
            DamageableRuntimeRegistry damageableRegistry)
        {
            _enemyStorage = enemyStorage;
            _targetRegistry = targetRegistry;
            _damageableRegistry = damageableRegistry;
        }

        public void Tick()
        {
            float deltaTime = Time.deltaTime;

            foreach (var enemy in _enemyStorage.Enemies)
            {
                if (!enemy.IsAlive)
                    continue;

                enemy.TickAttackCooldown(deltaTime);

                if (!enemy.TargetId.HasValue)
                    continue;

                if (!_targetRegistry.TryGet(enemy.TargetId.Value, out var target))
                    continue;

                float range = enemy.Config.AttackRange;
                float sqrDistance = (target.Position - enemy.Position).sqrMagnitude;

                if (sqrDistance > range * range)
                    continue;

                if (!enemy.CanAttack())
                    continue;

                if (!_damageableRegistry.TryGet(target.Id, out var damageable))
                    continue;

                damageable.TakeDamage(enemy.Config.AttackDamage);
                enemy.ResetAttackCooldown();
            }
        }
    }
}