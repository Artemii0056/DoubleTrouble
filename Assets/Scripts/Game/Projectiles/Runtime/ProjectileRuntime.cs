using System.Collections.Generic;
using Game.Combat.Targeting;
using Game.Projectiles.Effects;
using UnityEngine;

namespace Game.Projectiles.Runtime
{
    public class ProjectileRuntime : ITargetHitHistory
    {
        private const int InlineHitTargetCapacity = 4;

        private IReadOnlyList<IProjectileEffect> _effects;
        private int _hitTargetCount;
        private int _hitTargetId0;
        private int _hitTargetId1;
        private int _hitTargetId2;
        private int _hitTargetId3;
        private HashSet<int> _overflowHitTargetIds;

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
            IReadOnlyList<IProjectileEffect> effects)
        {
            Init(
                id,
                ownerId,
                position,
                direction,
                speed,
                radius,
                lifetime,
                pierceLeft,
                ricochetLeft,
                effects);
        }

        public int Id { get; private set; }
        public int OwnerId { get; private set; }

        public Vector3 Position { get; private set; }
        public Vector3 Direction { get; private set; }

        public float Speed { get; private set; }
        public float Radius { get; private set; }
        public float Lifetime { get; private set; }

        public bool IsAlive { get; private set; }

        public int PierceLeft { get; private set; }
        public int RicochetLeft { get; private set; }

        public IReadOnlyList<IProjectileEffect> Effects => _effects;

        public void Init(
            int id,
            int ownerId,
            Vector3 position,
            Vector3 direction,
            float speed,
            float radius,
            float lifetime,
            int pierceLeft,
            int ricochetLeft,
            IReadOnlyList<IProjectileEffect> effects)
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
            _effects = effects;
            IsAlive = true;
            ClearHitTargets();
        }

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
            return Contains(targetId);
        }

        public bool Contains(int targetId)
        {
            if (_hitTargetCount > 0 && _hitTargetId0 == targetId)
                return true;

            if (_hitTargetCount > 1 && _hitTargetId1 == targetId)
                return true;

            if (_hitTargetCount > 2 && _hitTargetId2 == targetId)
                return true;

            if (_hitTargetCount > 3 && _hitTargetId3 == targetId)
                return true;

            return _overflowHitTargetIds != null && _overflowHitTargetIds.Contains(targetId);
        }

        public void RegisterHit(int targetId)
        {
            if (Contains(targetId))
                return;

            if (_hitTargetCount < InlineHitTargetCapacity)
            {
                AddInlineHitTarget(targetId);
                return;
            }

            _overflowHitTargetIds ??= new HashSet<int>();
            _overflowHitTargetIds.Add(targetId);
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

        private void AddInlineHitTarget(int targetId)
        {
            switch (_hitTargetCount)
            {
                case 0:
                    _hitTargetId0 = targetId;
                    break;
                case 1:
                    _hitTargetId1 = targetId;
                    break;
                case 2:
                    _hitTargetId2 = targetId;
                    break;
                case 3:
                    _hitTargetId3 = targetId;
                    break;
            }

            _hitTargetCount++;
        }

        private void ClearHitTargets()
        {
            _hitTargetCount = 0;
            _hitTargetId0 = 0;
            _hitTargetId1 = 0;
            _hitTargetId2 = 0;
            _hitTargetId3 = 0;
            _overflowHitTargetIds?.Clear();
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
