using Game.Combat.Services;
using UnityEngine;

namespace Game.Projectiles.Effects
{
    [CreateAssetMenu(menuName = "Game/Projectiles/Effects/Magic Damage")]
    public sealed class MagicDamageEffectConfig : ProjectileEffectConfig
    {
        public float damage = 6f;

        public override IProjectileEffect CreateEffect() //Говно. Конфиги создают что-то
        {
            return new MagicDamageEffect(damage);
        }
    }
}