using Game.Characters.Enemy.Configs;
using UnityEngine;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntime : IDamageableRuntime
    {
        public EnemyRuntime(int id, EnemyConfig config, Vector3 position)
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
        public float AttackCooldownLeft { get; private set; }

        public void SetTarget(int targetId) => TargetId = targetId;

        public void Move(Vector3 direction, float deltaTime)
        {
            if (!IsAlive)
                return;

            direction.y = 0f;

            if (direction.sqrMagnitude <= 0.001f)
                return;

            Direction = direction.normalized;
            Position += Direction * Config.MoveSpeed * deltaTime;
        }

        public void TakeDamage(float damage)
        {
            Debug.Log($"Enemy {Id} take damage {damage}. HP before: {Hp}");

            if (!IsAlive)
                return;

            Hp -= damage;

            if (Hp <= 0f)
                Kill();
        }

        public void TickAttackCooldown(float deltaTime)
        {
            if (AttackCooldownLeft > 0f)
                AttackCooldownLeft -= deltaTime;
        }

        public bool CanAttack() =>
            AttackCooldownLeft <= 0f;

        public void ResetAttackCooldown() =>
            AttackCooldownLeft = Config.AttackCooldown;

        private void Kill()
        {
            IsAlive = false;
        }
    }
}