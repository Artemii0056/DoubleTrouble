using System.Collections.Generic;
using Game.Projectiles.Effects;
using UnityEngine;

namespace Game.Projectiles.Runtime
{
    public class ProjectileRuntime
    {
        public ProjectileRuntime(
            int id,
            int ownerId,
            Vector3 position,
            Vector3 direction,
            float speed,
            float radius,
            float lifetime,
            int pierceLeft,
            int ricochetLeft,
            IEnumerable<IProjectileEffect> effects, float damage)
        {
            Id = id;
            OwnerId = ownerId;
            Position = position;
            Direction = direction.normalized;
            Speed = speed;
            Radius = radius;
            Lifetime = lifetime;
            PierceLeft = pierceLeft;
            RicochetLeft = ricochetLeft;
            Damage = damage;

            _effects.AddRange(effects);
        }
    
        public int Id { get; }
        public int OwnerId { get; }

        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }
        public float  Damage { get; private set; }

        public float Speed { get; private set; }
        public float Radius { get; }
        public float Lifetime { get; private set; }

        public bool IsAlive { get; private set; } = true;

        public int PierceLeft { get; private set; }
        public int RicochetLeft { get; private set; }

        private readonly HashSet<int> _hitTargetIds = new();
        private readonly List<IProjectileEffect> _effects = new();

        public IReadOnlyCollection<int> HitTargetIds => _hitTargetIds;
        public IReadOnlyList<IProjectileEffect> Effects => _effects;

        public void Tick(float deltaTime)
        {
            if (!IsAlive)
                return;

            Move(deltaTime);
            ReduceLifetime(deltaTime);
        }

        public void Kill()
        {
            IsAlive = false;
        }

        public bool WasTargetHit(int targetId)
        {
            return _hitTargetIds.Contains(targetId);
        }

        public void RegisterHit(int targetId)
        {
            _hitTargetIds.Add(targetId);
        }

        public void SpendPierce()
        {
            PierceLeft--;

            if (PierceLeft < 0)
                Kill();
        }

        public bool TryRicochet(Vector3 newDirection)
        {
            if (RicochetLeft <= 0)
                return false;

            RicochetLeft--;
            Direction = newDirection.normalized;
            return true;
        }

        private void Move(float deltaTime)
        {
            Position += Direction * Speed * deltaTime;
        }

        private void ReduceLifetime(float deltaTime)
        {
            Lifetime -= deltaTime;

            if (Lifetime <= 0f)
                Kill();
        }
    }
}