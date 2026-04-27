using Game.Combat.Services;
using UnityEngine;

namespace Game.Projectiles.Effects
{
    [CreateAssetMenu(menuName = "Game/Projectiles/Effects/Slow")]
    public sealed class SlowEffectConfig : ProjectileEffectConfig
    {
        [Range(0f, 1f)]
        public float slowPower = 0.4f;

        public float duration = 2f;

        public override IProjectileEffect CreateEffect(CombatServices services)
        {
            return new SlowEffect(slowPower, duration);
        }
    }
}