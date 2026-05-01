using System.Collections.Generic;
using Game.Characters.Enemy.Configs;
using UnityEngine;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntimePool
    {
        private readonly Stack<EnemyRuntime> _pool = new();

        public EnemyRuntime Spawn(int id, EnemyConfig config, Vector3 position)
        {
            if (_pool.Count == 0)
                return new EnemyRuntime(id, config, position);

            EnemyRuntime runtime = _pool.Pop();
            runtime.Init(id, config, position);
            return runtime;
        }

        public void Release(EnemyRuntime runtime)
        {
            _pool.Push(runtime);
        }
    }
}
