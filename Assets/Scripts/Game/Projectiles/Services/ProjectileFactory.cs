using System.Collections.Generic;
using Game.Combat.Damage;
using Game.Combat.Statuses;
using Game.Projectiles.Effects;
using Game.Projectiles.Runtime;
using UnityEngine;

namespace Game.Projectiles.Services
{
    public class ProjectileFactory : IProjectileFactory
    {
        private int _nextId = 1;

        private readonly DamageService _damageService;
        private readonly StatusEffectService _statusEffectService;

        public ProjectileFactory(
            DamageService damageService,
            StatusEffectService statusEffectService)
        {
            _damageService = damageService;
            _statusEffectService = statusEffectService;
        }

        public ProjectileRuntime Create(
            ProjectileConfig config,
            int ownerId,
            Vector3 position,
            Vector3 direction)
        {
            var effects = new List<IProjectileEffect>();

            foreach (var effectConfig in config.effects)
            {
                switch (effectConfig)
                {
                    case MagicDamageEffectConfig magic:
                        effects.Add(new MagicDamageEffect(magic.damage, _damageService));
                        break;

                    case SlowEffectConfig slow:
                        // effects.Add(new SlowEffect(
                        //     slow.slowPower,
                        //     slow.duration,
                        //     _statusEffectService));
                        break;
                }
            }

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
                effects);
        }
    }
}