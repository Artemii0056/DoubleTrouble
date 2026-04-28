using Game.Characters.Enemy.Configs;
using Game.Characters.Enemy.Runtime;
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

        public EnemyFactory(
            DiContainer container,
            IGlobalServiceId idService,
            EnemyRuntimeStorage enemyStorage,
            EnemyViewRegistry viewRegistry)
        {
            _container = container;
            _idService = idService;
            _enemyStorage = enemyStorage;
            _viewRegistry = viewRegistry;
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

            _enemyStorage.Add(runtime);
            _viewRegistry.Register(id, view);

            return runtime;
        }
    }
}