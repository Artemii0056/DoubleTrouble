using System.Collections.Generic;
using TestSystem.TestProjectileLogic.Projectiles;
using UnityEngine;

namespace TestSystem.TestProjectileLogic.Effects
{
    [CreateAssetMenu(menuName = "Game/Projectiles/Projectile Config")]
    public sealed class ProjectileConfig : ScriptableObject
    {
        public ProjectileView Prefab;

        public float speed = 12f;
        public float radius = 0.25f;
        public float lifetime = 3f;

        public int pierce;
        public int ricochet;

        public List<ProjectileEffectConfig> effects;
    }
}