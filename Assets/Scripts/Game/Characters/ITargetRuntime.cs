using UnityEngine;

namespace Game.Characters
{
    public interface ITargetRuntime
    {
        int Id { get; }
        Vector3 Position { get; }
        bool IsAlive { get; }
    }
}