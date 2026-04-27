using UnityEngine;

namespace TestSystem
{
    public interface IRotationTargetProvider
    {
        bool TryGetRotation(out Vector3 direction);
    }
}