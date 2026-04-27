using ShootSystem.Scripts;
using TestSystem.TestProjectileLogic.Statuses;
using UnityEngine;

namespace TestSystem.TestProjectileLogic
{
    public class DamageService
    {
        // public void ApplyDamage(ITargetable target, DamageData damage)
        // {
        //     target.TakeDamage(damage);
        // }
        
        public void ApplyDamage(ITargetable target, DamageData damage)
        {
            Debug.Log(target.Id + " - " + damage);
            //target.TakeDamage(damage);
        }
    }
}