using Game.Characters.Enemy.Services;
using Game.Combat;
using Game.Combat.Targeting;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public sealed class ProjectileHitSystem
    {
        private const float RicochetSearchRadius = 8f;

        private readonly TargetSelectionService _targetSelection;
        private readonly DamageableRuntimeStore _damageableStore;

        public ProjectileHitSystem(
            TargetSelectionService targetSelection, 
            DamageableRuntimeStore damageableStore)
        {
            _targetSelection = targetSelection;
            _damageableStore = damageableStore;
        }

        public void HandleHit(ProjectileRuntime projectile, IAimTargetable target)
        {
            if (!projectile.IsAlive)
                return;

            if (target == null || target.AimPoint == null)
                return;

            int targetId = GetTargetId(target);

            if (!_damageableStore.TryGet(targetId, out var damageable))
                return;

            if (!damageable.IsAlive)
                return;

            var context = new ProjectileHitContext(
                projectile,
                target);

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

        private bool TryRicochet(ProjectileRuntime projectile, IAimTargetable currentTarget)
        {
            if (projectile.RicochetLeft <= 0)
                return false;

            int currentTargetId = GetTargetId(currentTarget);

            var nextTarget = _targetSelection.FindNearestExcept(
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

        private static int GetTargetId(IAimTargetable target)
        {
            return target.Id;
        }
    }
}