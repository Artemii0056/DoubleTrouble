using System.Collections.Generic;
using Game.Characters.Enemy.Configs;
using Game.Combat.Statuses;
using UnityEngine;

namespace Game.Characters.Enemy.Runtime
{
    public sealed class EnemyRuntime : IDamageable, IStatusable
    {
        private readonly List<StatusData> _statuses = new();
        
        public EnemyRuntime(int id, EnemyConfig config, Vector3 position)
        {
            Init(id, config, position);
        }

        public int Id { get; private set; }
        public EnemyConfig Config { get; private set; }

        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }

        public float Hp { get; private set; }
        public bool IsAlive { get; private set; }

        public int? TargetId { get; private set; }
        public float AttackCooldownLeft { get; private set; }

        public void Init(int id, EnemyConfig config, Vector3 position)
        {
            Id = id;
            Config = config;
            Position = position;
            Direction = Vector3.zero;
            Hp = config.MaxHp;
            IsAlive = true;
            TargetId = null;
            AttackCooldownLeft = 0f;
            _statuses.Clear();
        }

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

        public void ApplyStatus(StatusData status)
        {
            _statuses.Add(status);
        }
    }
}
