using UnityEngine;

namespace TestSystem.TestProjectileLogic.Effects
{
    [CreateAssetMenu(menuName = "Game/Projectiles/Effects/Magic Damage")]
    public sealed class MagicDamageEffectConfig : ProjectileEffectConfig
    {
        public float damage = 6f;

        public override IProjectileEffect CreateEffect(CombatServices services)
        {
            return new MagicDamageEffect(damage);
        }
    }
}