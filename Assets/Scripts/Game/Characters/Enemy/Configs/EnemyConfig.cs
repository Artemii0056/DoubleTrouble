using Game.Characters.Enemy.View;
using UnityEngine;

namespace Game.Characters.Enemy.Configs
{
    [CreateAssetMenu(fileName = nameof(EnemyConfig), menuName = "EnemySettings/" + nameof(EnemyConfig))]
    public sealed class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public EnemyTarget Type { get; private set; }
    
        [field: SerializeField] public EnemyView Prefab { get; private set; }
        [field: SerializeField] public float MaxHp { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackCooldown { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float TargetSearchInterval { get; private set; }
    }
}

