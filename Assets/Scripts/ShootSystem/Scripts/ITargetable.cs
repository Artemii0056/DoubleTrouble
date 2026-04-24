using UnityEngine;

namespace ShootSystem.Scripts
{
    public interface ITargetable
    {
        Transform AimPoint { get; }
        bool IsAlive { get; }
    }
}