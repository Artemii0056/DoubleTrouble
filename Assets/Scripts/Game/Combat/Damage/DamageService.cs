using Game.Combat.Statuses;
using UnityEngine;

namespace Game.Combat.Damage
{
    public class DamageService
    {
        // public void ApplyDamage(ITargetable target, DamageData damage)
        // {
        //     target.TakeDamage(damage);
        // }
        
        public void ApplyDamage(IAimTarget target, DamageData damage)
        {
            Debug.Log(target.Id + " - " + damage);
            //target.TakeDamage(damage);
        }
    }
}