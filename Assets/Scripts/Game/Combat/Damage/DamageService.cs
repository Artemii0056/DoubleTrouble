using Game.Characters;
using Game.Combat.Statuses;

namespace Game.Combat.Damage
{
    public class DamageService
    {
        public void ApplyDamage(IDamageable target, DamageData damage)
        {
            if (target == null || !target.IsAlive)
                return;

            if (damage.Amount <= 0f)
                return;

            target.TakeDamage(damage.Amount);
        }
    }
}
