using UnityEngine;

namespace Game.Projectiles.Effects
{
    [CreateAssetMenu(fileName = nameof(MagicDamageEffectConfig), menuName = "DamageConfig/" + nameof(MagicDamageEffectConfig))]
    public sealed class MagicDamageEffectConfig : ProjectileEffectConfig
    {
        public float damage = 6f;
    }
}