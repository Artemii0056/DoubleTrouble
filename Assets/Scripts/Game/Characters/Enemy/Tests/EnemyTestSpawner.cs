using Game.Characters.Enemy.Configs;
using UnityEngine;
using Zenject;

namespace Game.Characters.Enemy.Tests
{
    public sealed class EnemyTestSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private Transform[] _spawnPoint;

        private EnemyFactory _enemyFactory;

        [Inject]
        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        private void Start()
        {
            for (int i = 0; i < _spawnPoint.Length; i++) 
                _enemyFactory.Create(_config, _spawnPoint[i].position);
        }
    }
}