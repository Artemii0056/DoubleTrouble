using Game.Characters.Enemy.Runtime;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyAttackSystem : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly TargetRuntimeStore _targetStore;
        private readonly DamageableRuntimeStore _damageableStore;

        public EnemyAttackSystem(
            EnemyRuntimeStorage enemyStorage,
            TargetRuntimeStore targetStore,
            DamageableRuntimeStore damageableStore)
        {
            _enemyStorage = enemyStorage;
            _targetStore = targetStore;
            _damageableStore = damageableStore;
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

                if (!_targetStore.TryGet(enemy.TargetId.Value, out var target))
                    continue;

                float range = enemy.Config.AttackRange;
                float sqrDistance = (target.Position - enemy.Position).sqrMagnitude;

                if (sqrDistance > range * range)
                    continue;

                if (!enemy.CanAttack())
                    continue;

                if (!_damageableStore.TryGet(target.Id, out var damageable))
                    continue;

                damageable.TakeDamage(enemy.Config.AttackDamage);
                enemy.ResetAttackCooldown();
            }
        }
    }
}