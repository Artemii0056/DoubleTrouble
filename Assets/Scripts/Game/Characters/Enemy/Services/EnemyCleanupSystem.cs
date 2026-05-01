using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.View;
using Game.Combat.Targeting;
using Zenject;

namespace Game.Characters.Enemy.Services
{
    public sealed class EnemyCleanupSystem : ITickable
    {
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly EnemyViewStore _viewStore;
        private readonly CombatRegistry _combatRegistry;
        private readonly TargetSelectionService _targetSelection;

        public EnemyCleanupSystem(
            EnemyRuntimeStore enemyStore,
            EnemyViewStore viewStore,
            CombatRegistry combatRegistry,
            TargetSelectionService targetSelection)
        {
            _enemyStore = enemyStore;
            _viewStore = viewStore;
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

                Cleanup(enemy);
                _enemyStore.RemoveAt(i);
            }
        }

        private void Cleanup(EnemyRuntime enemy)
        {
            _combatRegistry.UnregisterDamageable(enemy.Id);

            if (!_viewStore.TryGet(enemy.Id, out EnemyView view))
                return;

            view.SetDeadVisual();

            EnemyAimTarget aimTarget = view.GetComponentInChildren<EnemyAimTarget>();

            if (aimTarget != null)
                _targetSelection.Unregister(aimTarget);

            _viewStore.Unregister(enemy.Id);
        }
    }
}
