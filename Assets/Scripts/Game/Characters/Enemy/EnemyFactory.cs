using Game.Characters.Enemy.Configs;
using Game.Characters.Enemy.Runtime;
using Game.Characters.Enemy.Services;
using Game.Characters.Enemy.View;
using Game.Core.IdServices;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy
{
    public sealed class EnemyFactory
    {
        private readonly DiContainer _container;
        private readonly IGlobalServiceId _idService;
        private readonly EnemyRuntimeStorage _enemyStorage;
        private readonly EnemyViewRegistry _viewRegistry;
        
        private readonly TargetRuntimeRegistry _targetRegistry;
        private readonly DamageableRuntimeRegistry _damageableRegistry;

        public EnemyFactory(
            DiContainer container,
            IGlobalServiceId idService,
            EnemyRuntimeStorage enemyStorage,
            EnemyViewRegistry viewRegistry, TargetRuntimeRegistry targetRegistry, DamageableRuntimeRegistry damageableRegistry)
        {
            _container = container;
            _idService = idService;
            _enemyStorage = enemyStorage;
            _viewRegistry = viewRegistry;
            _targetRegistry = targetRegistry;
            _damageableRegistry = damageableRegistry;
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

            EnemyTarget enemyTarget = view.GetComponentInChildren<EnemyTarget>();

            if (enemyTarget != null)
                enemyTarget.Init(id);
            else
                Debug.LogError($"EnemyTarget not found on enemy prefab: {config.Prefab.name}");

            _enemyStorage.Add(runtime);
            _viewRegistry.Register(id, view);
            _damageableRegistry.Register(runtime);
            _viewRegistry.Register(id, view);

            return runtime;
        }
    }
}