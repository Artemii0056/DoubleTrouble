using UnityEngine;

namespace TestSystem.TestProjectileLogic.Effects
{
    public abstract class ProjectileEffectConfig : ScriptableObject
    {
        public abstract IProjectileEffect CreateEffect(CombatServices services);
    }
}