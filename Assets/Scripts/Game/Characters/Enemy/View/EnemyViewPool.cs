using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.View
{
    public sealed class EnemyViewPool
    {
        private readonly DiContainer _container;
        private readonly Dictionary<EnemyView, Stack<EnemyView>> _pools = new();
        private readonly Dictionary<EnemyView, EnemyView> _prefabsByView = new();

        public EnemyViewPool(DiContainer container)
        {
            _container = container;
        }

        public EnemyView Spawn(EnemyView prefab, int runtimeId, Vector3 position)
        {
            EnemyView view = GetView(prefab);

            view.transform.SetPositionAndRotation(position, Quaternion.identity);
            view.gameObject.SetActive(true);
            view.Init(runtimeId);

            return view;
        }

        public void Release(EnemyView view)
        {
            if (view == null)
                return;

            if (!_prefabsByView.TryGetValue(view, out EnemyView prefab))
                return;

            view.Deactivate();

            if (!_pools.TryGetValue(prefab, out var pool))
            {
                pool = new Stack<EnemyView>();
                _pools.Add(prefab, pool);
            }

            pool.Push(view);
        }

        private EnemyView GetView(EnemyView prefab)
        {
            if (_pools.TryGetValue(prefab, out var pool) && pool.Count > 0)
                return pool.Pop();

            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(prefab);
            _prefabsByView[view] = prefab;

            return view;
        }
    }
}
