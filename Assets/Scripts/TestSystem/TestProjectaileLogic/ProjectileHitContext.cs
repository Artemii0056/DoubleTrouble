using ShootSystem.Scripts;
using TestSystem.TestProjectaileLogic;

public readonly struct ProjectileHitContext
{
    public readonly ProjectileRuntime Projectile;
    public readonly ITargetable Target;
    public readonly CombatServices Services;

    public ProjectileHitContext(
        ProjectileRuntime projectile,
        ITargetable target,
        CombatServices services)
    {
        Projectile = projectile;
        Target = target;
        Services = services;
    }
}