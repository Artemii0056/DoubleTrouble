using UnityEngine;

namespace Game.Projectiles.Effects
{
    [CreateAssetMenu(fileName = nameof(SlowEffectConfig), menuName = "DamageConfig/" + nameof(SlowEffectConfig))]
    public sealed class SlowEffectConfig : ProjectileEffectConfig
    {
        [Range(0f, 1f)]
        public float slowPower = 0.4f;

        public float duration = 2f;
    }
}