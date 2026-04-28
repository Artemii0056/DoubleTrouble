using Zenject;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyViewSyncService : ITickable
    {
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly EnemyViewRegistry _viewRegistry;

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
                if (!_viewRegistry.TryGet(enemy.Id, out var view))
                    continue;

                view.SetPosition(enemy.Position);
                view.SetDirection(enemy.Direction);
            }
        }
    }
}