using Game.Combat.Damage;
using Game.Combat.Statuses;
using Game.Combat.Targeting;

namespace Game.Combat.Services
{
    public sealed class CombatServices
    {
        public CombatServices(
            DamageService damage,
            StatusEffectService statusEffects,
            TargetSearchService targetSearch)
        {
            Damage = damage;
            StatusEffects = statusEffects;
            TargetSearch = targetSearch;
        }
    
        public DamageService Damage { get; }
        public StatusEffectService StatusEffects { get; }
        public TargetSearchService TargetSearch { get; }
    }
}