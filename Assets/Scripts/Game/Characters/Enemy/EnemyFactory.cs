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
        private readonly EnemyViewStore _viewStore;
        
        private readonly TargetRuntimeStore _targetStore;
        private readonly DamageableRuntimeStore _damageableStore;

        public EnemyFactory(
            DiContainer container,
            IGlobalServiceId idService,
            EnemyRuntimeStorage enemyStorage,
            EnemyViewStore viewStore, TargetRuntimeStore targetStore, DamageableRuntimeStore damageableStore)
        {
            _container = container;
            _idService = idService;
            _enemyStorage = enemyStorage;
            _viewStore = viewStore;
            _targetStore = targetStore;
            _damageableStore = damageableStore;
        }

        public Runtime.Enemy Create(EnemyConfig config, Vector3 position)
        {
            int id = _idService.Next();

            Runtime.Enemy runtime = new Runtime.Enemy(id, config, position);

            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(
                config.Prefab,
                position,
                Quaternion.identity,
                null);

            view.Init(id);

            EnemyAimTarget enemyAimTarget = view.GetComponentInChildren<EnemyAimTarget>();

            if (enemyAimTarget != null)
                enemyAimTarget.Init(id);

            _enemyStorage.Add(runtime);
            _targetStore.Register(runtime);
            _damageableStore.Register(runtime);
            _viewStore.Register(id, view);

            return runtime;
        }
    }
}