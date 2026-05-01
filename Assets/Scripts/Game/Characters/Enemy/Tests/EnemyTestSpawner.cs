using Game.Characters.Enemy.Configs;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Characters.Enemy.Tests
{
    public sealed class EnemyTestSpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _config;
        [FormerlySerializedAs("_spawnPoint")] [SerializeField] private Transform[] _spawnPoints;

        private EnemyFactory _enemyFactory;

        [Inject]
        public void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        private void Start()
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
                _enemyFactory.Create(_config, _spawnPoints[i].position);
        }
    }
}
