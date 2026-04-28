using UnityEngine;

namespace Game.Characters
{
    public interface ITarget
    {
        int Id { get; }
        Vector3 Position { get; }
        bool IsAlive { get; }
    }
}