using Game.Combat.Statuses;
using Game.Projectiles.Runtime;

namespace Game.Projectiles.Effects
{
    public sealed class SlowEffect : IProjectileEffect
    {
        private readonly float _power;
        private readonly float _duration;

        public SlowEffect(float power, float duration)
        {
            _power = power;
            _duration = duration;
        }

        public void OnHit(ProjectileHitContext context)
        {
            context.Services.StatusEffects.ApplyStatus(
                context.Target,
                new StatusData(StatusType.Slow, _power, _duration)
            );
        }
    }
}