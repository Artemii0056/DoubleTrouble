using System.Collections.Generic;
using TestSystem.TestProjectaileLogic;
using UnityEngine;

public sealed class ProjectileSimulationService
{
    public readonly List<ProjectileRuntime> Projectiles = new();

    public void Tick(float deltaTime)
    {
        foreach (var projectile in Projectiles)
        {
            if (!projectile.IsAlive)
                continue;

            projectile.Position += projectile.Direction * projectile.Speed * deltaTime;
            projectile.Lifetime -= deltaTime;

            if (projectile.Lifetime <= 0f)
                projectile.IsAlive = false;
        }

        Projectiles.RemoveAll(p => !p.IsAlive);
    }

    public void Add(ProjectileRuntime projectile)
    {
        Projectiles.Add(projectile);
    }
}