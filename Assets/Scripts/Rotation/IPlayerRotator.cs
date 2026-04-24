using UnityEngine;

namespace Rotation
{
    public interface IPlayerRotator
    {
        void Init(Transform player);
        void Tick();
    }
}