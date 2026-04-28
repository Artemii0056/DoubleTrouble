using UnityEngine;

namespace Game.Combat
{
    public interface IAimTargetable
    {
        int Id { get; }
        Transform AimPoint { get; }
    }
}