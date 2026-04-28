using System.Collections.Generic;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntimeStorage
    {
        private readonly List<Enemy> _enemies = new();

        public IReadOnlyList<Enemy> Enemies => _enemies;

        public void Add(Enemy enemy)
        {
            _enemies.Add(enemy);
        }

        public void RemoveDead()
        {
            _enemies.RemoveAll(enemy => !enemy.IsAlive);
        }
    }
}