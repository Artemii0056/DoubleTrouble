using System.Collections.Generic;
using TestSystem.TestProjectaileLogic;
using UnityEngine;

public sealed class ProjectileFactory
{
    private int _nextId = 1;

    private readonly CombatServices _combatServices;

    public ProjectileFactory(CombatServices combatServices)
    {
        _combatServices = combatServices;
    }

    public ProjectileRuntime Create(
        ProjectileConfig config,
        int ownerId,
        Vector3 position,
        Vector3 direction)
    {
        var projectile = new ProjectileRuntime
        {
            Id = _nextId++,
            OwnerId = ownerId,
            Position = position,
            Direction = direction.normalized,
            Speed = config.speed,
            Radius = config.radius,
            Lifetime = config.lifetime,
            PierceLeft = config.pierce,
            RicochetLeft = config.ricochet
        };

        foreach (var effectConfig in config.effects)
        {
            projectile.Effects.Add(effectConfig.CreateEffect(_combatServices));
        }

        return projectile;
    }
}