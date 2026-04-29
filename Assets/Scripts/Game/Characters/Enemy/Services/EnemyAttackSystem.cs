using Game.Characters.Enemy.Runtime;
using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyAttackSystem : ITickable
    {
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly CombatRegistry _combatRegistry;

        public EnemyAttackSystem(
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

                enemy.TickAttackCooldown(deltaTime);

                if (!enemy.TargetId.HasValue)
                    continue;

                if (!_combatRegistry.TryGetTarget(enemy.TargetId.Value, out var target))
                    continue;

                float range = enemy.Config.AttackRange;
                float sqrDistance = (target.Position - enemy.Position).sqrMagnitude;

                if (sqrDistance > range * range)
                    continue;

                if (!enemy.CanAttack())
                    continue;

                if (!_combatRegistry.TryGetDamageable(target.Id, out var damageable))
                    continue;

                damageable.TakeDamage(enemy.Config.AttackDamage);
                enemy.ResetAttackCooldown();
            }
        }
    }
}
