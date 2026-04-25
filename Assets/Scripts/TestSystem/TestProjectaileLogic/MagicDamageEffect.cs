public sealed class MagicDamageEffect : IProjectileEffect
{
    private readonly float _damage;

    public MagicDamageEffect(float damage)
    {
        _damage = damage;
    }

    public void OnHit(ProjectileHitContext context)
    {
        context.Services.Damage.ApplyDamage(
            context.Target,
            new DamageData(
                context.Projectile.OwnerId,
                _damage,
                DamageType.Magic
            )
        );
    }
}