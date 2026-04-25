using UnityEngine;

public abstract class ProjectileEffectConfig : ScriptableObject
{
    public abstract IProjectileEffect CreateEffect(CombatServices services);
}