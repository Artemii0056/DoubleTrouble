using Game.Characters.Enemy.Configs;
using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.View;
using Game.Combat.Targeting;
using Game.Core.IdServices;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy
{
    public sealed class EnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IGlobalServiceId _idService;
        private readonly EnemyRuntimeStore _enemyStore;
        private readonly EnemyViewStore _viewStore;
        private readonly CombatRegistry _combatRegistry;

        public EnemyFactory(
            DiContainer container,
            IGlobalServiceId idService,
            EnemyRuntimeStore enemyStore,
            EnemyViewStore viewStore,
            CombatRegistry combatRegistry)
        {
            _container = container;
            _idService = idService;
            _enemyStore = enemyStore;
            _viewStore = viewStore;
            _combatRegistry = combatRegistry;
        }

        public EnemyRuntime Create(EnemyConfig config, Vector3 position)
        {
            int id = _idService.Next();

            EnemyRuntime runtime = new EnemyRuntime(id, config, position);

            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(
                config.Prefab,
                position,
                Quaternion.identity,
                null);

            view.Init(id);

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
