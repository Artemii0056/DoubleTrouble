using Game.Combat;
using Game.Combat.Statuses;
using Game.Combat.Targeting;
using Game.Projectiles.Effects;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public sealed class ProjectileHitSystem
    {
        private const float RicochetSearchRadius = 8f;

        private readonly TargetSelectionService _targetSelection;
        private readonly CombatRegistry _combatRegistry;

        public ProjectileHitSystem(
            TargetSelectionService targetSelection,
            CombatRegistry combatRegistry)
        {
            _targetSelection = targetSelection;
            _combatRegistry = combatRegistry;
        }

        public void HandleHit(ProjectileRuntime projectile, IAimTarget target)
        {
            if (!projectile.IsAlive)
                return;

            if (target == null || target.AimPoint == null)
                return;

            int targetId = target.Id;

            if (projectile.WasTargetHit(targetId))
                return;

            if (!_combatRegistry.TryGetDamageable(targetId, out var damageable))
                return;

            if (!damageable.IsAlive)
                return;

            projectile.RegisterHit(targetId);

            IStatusable statusable = damageable as IStatusable;

            var context = new ProjectileHitContext(
                projectile,
                target,
                damageable,
                statusable);

            foreach (var effect in projectile.Effects)
                effect.OnHit(context);

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

        private bool TryRicochet(ProjectileRuntime projectile, IAimTarget currentTarget)
        {
            if (projectile.RicochetLeft <= 0)
                return false;

            int currentTargetId = currentTarget.Id;

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
    }
}
