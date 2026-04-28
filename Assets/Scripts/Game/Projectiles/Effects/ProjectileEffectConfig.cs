using UnityEngine;

namespace Game.Projectiles.Effects
{
    public abstract class ProjectileEffectConfig : ScriptableObject
    {
        public abstract IProjectileEffect CreateEffect();
    }
}