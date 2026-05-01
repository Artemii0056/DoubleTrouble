using System.Collections.Generic;
using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.View;
using Game.Combat.Targeting;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyCleanupSystem : ITickable
    {
        private const float DeadViewLifetime = 1f;

        private readonly EnemyRuntimeStore _enemyStore;
        private readonly EnemyRuntimePool _runtimePool;
        private readonly EnemyViewStore _viewStore;
        private readonly EnemyViewPool _viewPool;
        private readonly CombatRegistry _combatRegistry;
        private readonly TargetSelectionService _targetSelection;
        private readonly Dictionary<int, float> _removeTimes = new();

        public EnemyCleanupSystem(
            EnemyRuntimeStore enemyStore,
            EnemyRuntimePool runtimePool,
            EnemyViewStore viewStore,
            EnemyViewPool viewPool,
            CombatRegistry combatRegistry,
            TargetSelectionService targetSelection)
        {
            _enemyStore = enemyStore;
            _runtimePool = runtimePool;
            _viewStore = viewStore;
            _viewPool = viewPool;
            _combatRegistry = combatRegistry;
            _targetSelection = targetSelection;
        }

        public void Tick()
        {
            for (int i = _enemyStore.Enemies.Count - 1; i >= 0; i--)
            {
                EnemyRuntime enemy = _enemyStore.Enemies[i];

                if (enemy.IsAlive)
                    continue;

                if (!_removeTimes.ContainsKey(enemy.Id))
                {
                    BeginCleanup(enemy);
                    continue;
                }

                if (Time.time < _removeTimes[enemy.Id])
                    continue;

                CompleteCleanup(enemy);
                _enemyStore.RemoveAt(i);
                _runtimePool.Release(enemy);
            }
        }

        private void BeginCleanup(EnemyRuntime enemy)
        {
            _removeTimes[enemy.Id] = Time.time + DeadViewLifetime;
            _combatRegistry.UnregisterDamageable(enemy.Id);

            if (!_viewStore.TryGet(enemy.Id, out EnemyView view))
                return;

            view.SetDeadVisual();

            EnemyAimTarget aimTarget = view.GetComponentInChildren<EnemyAimTarget>();

            if (aimTarget != null)
                _targetSelection.Unregister(aimTarget);
        }

        private void CompleteCleanup(EnemyRuntime enemy)
        {
            _removeTimes.Remove(enemy.Id);

            if (!_viewStore.TryGet(enemy.Id, out EnemyView view))
                return;

            _viewStore.Unregister(enemy.Id);
            _viewPool.Release(view);
        }
    }
}
