using System.Collections.Generic;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Effects
{
    [CreateAssetMenu(fileName = nameof(ProjectileConfig), menuName = "DamageConfig/" + nameof(ProjectileConfig))]
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