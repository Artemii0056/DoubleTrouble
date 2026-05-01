using System.Collections.Generic;
using Game.Combat.Damage;
using Game.Projectiles.Effects;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly Dictionary<ProjectileConfig, IReadOnlyList<IProjectileEffect>> _effectCache = new();
        private readonly DamageService _damageService;

        private int _nextId = 1;

        public ProjectileFactory(DamageService damageService)
        {
            _damageService = damageService;
        }

        public ProjectileRuntime Create(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction)
        {
            return new ProjectileRuntime(
                _nextId++,
                ownerId,
                position,
                direction.normalized,
                config.speed,
                config.radius,
                config.lifetime,
                config.pierce,
                config.ricochet,
                GetEffects(config));
        }

        private IReadOnlyList<IProjectileEffect> GetEffects(ProjectileConfig config)
        {
            if (_effectCache.TryGetValue(config, out var effects))
                return effects;

            effects = CreateEffects(config);
            _effectCache.Add(config, effects);

            return effects;
        }

        private IReadOnlyList<IProjectileEffect> CreateEffects(ProjectileConfig config)
        {
            var effects = new List<IProjectileEffect>();

            if (config.effects == null)
                return effects;

            foreach (var effectConfig in config.effects)
            {
                switch (effectConfig)
                {
                    case MagicDamageEffectConfig magic:
                        effects.Add(new MagicDamageEffect(magic.damage, _damageService));
                        break;

                    case SlowEffectConfig:
                        break;
                }
            }

            return effects;
        }
    }
}
