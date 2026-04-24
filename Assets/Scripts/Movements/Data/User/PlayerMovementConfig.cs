using UnityEngine;

namespace Movements.Data.User
{
    [CreateAssetMenu(fileName = nameof(PlayerMovementConfig), menuName = "PlayerSettings/" + nameof(PlayerMovementConfig))]
    public class PlayerMovementConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField] public float Gravity { get; private set; } = -9.81f;
        [field: SerializeField] public float GroundedVerticalVelocity { get; private set; } = -2f;
    }
}