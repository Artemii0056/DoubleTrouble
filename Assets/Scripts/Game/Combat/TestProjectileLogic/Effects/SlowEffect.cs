using TestSystem.TestProjectileLogic.Projectiles;
using TestSystem.TestProjectileLogic.Statuses;

namespace TestSystem.TestProjectileLogic.Effects
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