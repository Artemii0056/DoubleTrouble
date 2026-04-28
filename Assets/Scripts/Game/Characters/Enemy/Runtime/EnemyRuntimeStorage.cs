using System.Collections.Generic;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntimeStorage
    {
        private readonly List<EnemyRuntime> _enemies = new();

        public IReadOnlyList<EnemyRuntime> Enemies => _enemies;

        public void Add(EnemyRuntime enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveDead()
        {
            _enemies.RemoveAll(enemy => !enemy.IsAlive);
        }
    }
}