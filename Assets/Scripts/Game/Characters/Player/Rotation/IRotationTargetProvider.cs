using UnityEngine;

namespace Game.Characters.Player.Rotation
{
    public interface IRotationTargetProvider
    {
        bool TryGetRotation(out Vector3 direction);
    }
}