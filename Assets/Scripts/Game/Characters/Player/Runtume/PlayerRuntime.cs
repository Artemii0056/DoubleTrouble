using Game.Characters.Enemy.Runtime;
using UnityEngine;

public sealed class PlayerRuntime : ITargetRuntime
{
    public int Id { get; }
    public Vector3 Position { get; private set; }
    public bool IsAlive => true;

    public void SetPosition(Vector3 position)
    {
        Position = position;
    }
}