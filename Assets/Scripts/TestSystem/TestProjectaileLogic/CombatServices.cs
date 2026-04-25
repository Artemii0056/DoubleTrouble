using TestSystem.TestProjectaileLogic;

public sealed class CombatServices
{
    public DamageService Damage { get; }
    public StatusEffectService StatusEffects { get; }
    public TargetSearchService TargetSearch { get; }

    public CombatServices(
        DamageService damage,
        StatusEffectService statusEffects,
        TargetSearchService targetSearch)
    {
        Damage = damage;
        StatusEffects = statusEffects;
        TargetSearch = targetSearch;
    }
}