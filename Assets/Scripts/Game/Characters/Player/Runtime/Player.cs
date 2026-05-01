using Game.Combat;
using UnityEngine;

namespace Game.Characters.Player.Runtime
{
    public sealed class Player : IDamageable
    {
        private readonly Health _health = new();

        public Player(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public Vector3 Position { get; private set; }
        public bool IsAlive => _health.IsAlive;

        public void SetPosition(Vector3 position)
        {
            Position = position;
        }

        public void TakeDamage(float damage)
        {
            _health.DecreaseValue(damage);
        }
    }
}
