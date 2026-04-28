using System.Collections.Generic;
using Game.Characters.Enemy.View;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyViewSyncService : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly EnemyViewStore _viewStore;
        private readonly HashSet<int> _deadVisualApplied = new();

        public EnemyViewSyncService(
            EnemyRuntimeStorage enemyStorage,
            EnemyViewStore viewStore)
        {
            _enemyStorage = enemyStorage;
            _viewStore = viewStore;
        }

        public void Tick()
        {
            foreach (var enemy in _enemyStorage.Enemies)
            {
                if (!_viewStore.TryGet(enemy.Id, out var view))
                    continue;

                view.SetPosition(enemy.Position);

                if (!enemy.IsAlive)
                {
                    view.SetDeadVisual();
                    continue;
                }

                view.SetDirection(enemy.Direction);
            }
        }
    }
}