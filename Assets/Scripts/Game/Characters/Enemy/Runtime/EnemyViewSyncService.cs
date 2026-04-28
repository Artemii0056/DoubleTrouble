using System.Collections.Generic;
using Game.Characters.Enemy.View;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyViewSyncService : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly EnemyViewRegistry _viewRegistry;
        private readonly HashSet<int> _deadVisualApplied = new();

        public EnemyViewSyncService(
            EnemyRuntimeStorage enemyStorage,
            EnemyViewRegistry viewRegistry)
        {
            _enemyStorage = enemyStorage;
            _viewRegistry = viewRegistry;
        }

        public void Tick()
        {
            foreach (var enemy in _enemyStorage.Enemies)
            {
                Debug.Log($"Enemy {enemy.Id} alive: {enemy.IsAlive}, hp: {enemy.Hp}");

                if (!_viewRegistry.TryGet(enemy.Id, out var view))
                {
                    Debug.LogWarning($"No view for enemy {enemy.Id}");
                    continue;
                }

                view.SetPosition(enemy.Position);

                if (!enemy.IsAlive)
                {
                    Debug.Log($"Enemy {enemy.Id} is dead, applying visual");
                    view.SetDeadVisual();
                    continue;
                }

                view.SetDirection(enemy.Direction);
            }
        }
    }
}