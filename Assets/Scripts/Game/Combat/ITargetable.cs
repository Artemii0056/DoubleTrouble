using UnityEngine;

namespace Game.Combat
{
    public interface ITargetable
    {
        int Id { get; }
        Transform AimPoint { get; }
        bool IsAlive { get; }
        Vector3 Position { get; }
    }
}