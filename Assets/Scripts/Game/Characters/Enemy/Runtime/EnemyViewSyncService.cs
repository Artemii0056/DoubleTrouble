using Game.Characters.Enemy.View;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyViewSyncService : ITickable
    {
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly EnemyViewStore _viewStore;

        public EnemyViewSyncService(
            EnemyRuntimeStore enemyStore,
            EnemyViewStore viewStore)
        {
            _enemyStore = enemyStore;
            _viewStore = viewStore;
        }

        public void Tick()
        {
            foreach (var enemy in _enemyStore.Enemies)
            {
                if (!_viewStore.TryGet(enemy.Id, out var view))
                    continue;

                if (!enemy.IsAlive)
                    continue;

                view.SetPosition(enemy.Position);
                view.SetDirection(enemy.Direction);
            }
        }
    }
}
