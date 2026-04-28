using Game.Characters.Enemy.Configs;
using UnityEngine;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntime : ITargetRuntime
    {
        public EnemyRuntime(
            int id,
            EnemyConfig config,
            Vector3 position)
        {
            Id = id;
            Config = config;
            Position = position;
            Hp = config.MaxHp;
        }

        public int Id { get; }
        public EnemyConfig Config { get; }

        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }

        public float Hp { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public int? TargetId { get; private set; }

        public void SetTarget(int targetId)
        {
            TargetId = targetId;
        }

        public void Move(Vector3 direction, float deltaTime)
        {
            Direction = direction.normalized;
            Position += Direction * Config.MoveSpeed * deltaTime;
        }

        public void TakeDamage(float damage)
        {
            Hp -= damage;

            if (Hp <= 0)
                Kill();
        }

        public void Kill()
        {
            IsAlive = false;
        }
    }

    public interface ITargetRuntime
    {
        int Id { get; }
        Vector3 Position { get; }
        bool IsAlive { get; }
    }
}