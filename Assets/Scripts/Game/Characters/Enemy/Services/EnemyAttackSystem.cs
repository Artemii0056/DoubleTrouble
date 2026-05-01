using Game.Characters.Enemy.Runtime;
using Game.Combat.Damage;
using Game.Combat.Statuses;
using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyAttackSystem : ITickable
    {
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly CombatRegistry _combatRegistry;
        private readonly DamageService _damageService;

        public EnemyAttackSystem(
            EnemyRuntimeStore enemyStore,
            CombatRegistry combatRegistry,
            DamageService damageService)
        {
            _enemyStore = enemyStore;
            _combatRegistry = combatRegistry;
            _damageService = damageService;
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

                _damageService.ApplyDamage(
                    damageable,
                    new DamageData(
                        enemy.Id,
                        enemy.Config.AttackDamage,
                        DamageType.Physical));

                enemy.ResetAttackCooldown();
            }
        }
    }
}
