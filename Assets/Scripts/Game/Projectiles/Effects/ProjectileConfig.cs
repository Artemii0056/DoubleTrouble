using System.Collections.Generic;
using Game.Projectiles.Runtime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Game.Projectiles.Effects
{
    [CreateAssetMenu(menuName = "Game/Projectiles/Projectile Config")]
    public sealed class ProjectileConfig : ScriptableObject
    {
        public ProjectileView Prefab;

        public float speed = 12f;
        public float radius = 0.25f;
        public float lifetime = 3f;
        public float damage = 10;

        public int pierce;
        public int ricochet;

        public List<ProjectileEffectConfig> effects;
    }
}