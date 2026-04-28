using Game.Characters.Enemy.Services;
using Game.Combat;
using Game.Combat.Services;
using Game.Combat.Targeting;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public sealed class ProjectileHitService
    {
        private const float RicochetSearchRadius = 8f;

        private readonly CombatServices _combatServices;
        private readonly TargetSearchService _targetSearch;
        private readonly DamageableRuntimeRegistry _damageableRegistry;

        public ProjectileHitService(
            CombatServices combatServices,
            TargetSearchService targetSearch, 
            DamageableRuntimeRegistry damageableRegistry)
        {
            _combatServices = combatServices;
            _targetSearch = targetSearch;
            _damageableRegistry = damageableRegistry;
        }

        public void HandleHit(ProjectileRuntime projectile, ITargetable target)
        {
            if (!projectile.IsAlive)
                return;

            if (target == null || !target.IsAlive || target.AimPoint == null)
                return;

            int targetId = GetTargetId(target);

            if (projectile.WasTargetHit(targetId))
                return;

            projectile.RegisterHit(targetId);
            
            if (_damageableRegistry.TryGet(targetId, out var damageable))
            {
                damageable.TakeDamage(projectile.Damage);
            }
            else
            {
                Debug.LogWarning($"Damageable target not found. TargetId: {targetId}");
            }

            var context = new ProjectileHitContext(
                projectile,
                target,
                _combatServices
            );

            foreach (var effect in projectile.Effects)
            {
                effect.OnHit(context);
            }

            if (TryRicochet(projectile, target))
                return;

            if (TryPierce(projectile))
                return;

            projectile.Kill();
        }

        private bool TryPierce(ProjectileRuntime projectile)
        {
            if (projectile.PierceLeft <= 0)
                return false;

            projectile.SpendPierce();
            return projectile.IsAlive;
        }

        private bool TryRicochet(ProjectileRuntime projectile, ITargetable currentTarget)
        {
            if (projectile.RicochetLeft <= 0)
                return false;

            int currentTargetId = GetTargetId(currentTarget);

            var nextTarget = _targetSearch.FindNearestExcept(
                projectile.Position,
                currentTargetId,
                projectile.HitTargetIds,
                RicochetSearchRadius
            );

            if (nextTarget == null || nextTarget.AimPoint == null)
                return false;

            Vector3 direction = nextTarget.AimPoint.position - projectile.Position;

            if (direction.sqrMagnitude <= 0.001f)
                return false;

            return projectile.TryRicochet(direction);
        }

        private static int GetTargetId(ITargetable target)
        {
            return target.Id;
        }
    }
}