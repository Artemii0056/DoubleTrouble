using UnityEngine;

namespace Game.Combat
{
    public interface IAimTarget
    {
        int Id { get; }
        Transform AimPoint { get; }
    }
}