using System.Collections.Generic;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntimeStore
    {
        private readonly List<EnemyRuntime> _enemies = new();

        public IReadOnlyList<EnemyRuntime> Enemies => _enemies;

        public void Add(EnemyRuntime enemyRuntime)
        {
            _enemies.Add(enemyRuntime);
        }

        public void RemoveDead()
        {
            _enemies.RemoveAll(enemy => !enemy.IsAlive);
        }
    }
}