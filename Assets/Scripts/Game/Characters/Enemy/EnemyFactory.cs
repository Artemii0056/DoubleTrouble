using Game.Characters.Enemy.Configs;
using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.View;
using Game.Combat.Targeting;
using Game.Core.IdServices;
using UnityEngine;

namespace Game.Characters.Enemy
{
    public sealed class EnemyFactory
    {
        private readonly IGlobalServiceId _idService;
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly EnemyRuntimePool _runtimePool;
        private readonly EnemyViewStore _viewStore;
        private readonly EnemyViewPool _viewPool;
        private readonly CombatRegistry _combatRegistry;

        public EnemyFactory(
            IGlobalServiceId idService,
            EnemyRuntimeStore enemyStore,
            EnemyRuntimePool runtimePool,
            EnemyViewStore viewStore,
            EnemyViewPool viewPool,
            CombatRegistry combatRegistry)
        {
            _idService = idService;
            _enemyStore = enemyStore;
            _runtimePool = runtimePool;
            _viewStore = viewStore;
            _viewPool = viewPool;
            _combatRegistry = combatRegistry;
        }

        public EnemyRuntime Create(EnemyConfig config, Vector3 position)
        {
            int id = _idService.Next();

            EnemyRuntime runtime = _runtimePool.Spawn(id, config, position);
            EnemyView view = _viewPool.Spawn(config.Prefab, id, position);

            EnemyAimTarget aimTarget = view.GetComponentInChildren<EnemyAimTarget>();

            if (aimTarget == null)
                Debug.LogError($"EnemyAimTarget not found on prefab {config.Prefab.name}");
            else
                aimTarget.Init(id);

            _enemyStore.Add(runtime);
            _combatRegistry.RegisterDamageable(runtime);
            _viewStore.Register(id, view);

            return runtime;
        }
    }
}
