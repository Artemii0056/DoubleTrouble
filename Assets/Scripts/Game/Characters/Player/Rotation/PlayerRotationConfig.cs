using UnityEngine;

namespace Game.Characters.Player.Rotation
{
    [CreateAssetMenu(fileName = nameof(PlayerRotationConfig), menuName = "PlayerSettings/" + nameof(PlayerRotationConfig))]
    public sealed class PlayerRotationConfig : ScriptableObject
    {
        [field: SerializeField] public float LookSensitivity { get; private set; } = 10f;
        [field: SerializeField] public float MinPitch { get; private set; } = -80f;
        [field: SerializeField] public float MaxPitch { get; private set; } = 80f;
        [field: SerializeField] public float RotationSpeed { get; private set; } = 10f;
    }
}
