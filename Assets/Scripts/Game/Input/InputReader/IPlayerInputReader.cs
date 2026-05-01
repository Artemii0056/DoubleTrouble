using System;
using UnityEngine;

namespace Game.Input.InputReader
{
    public interface IPlayerInputReader
    {
        event Action InteractStarted;
        event Action InteractCanceled;

        Vector2 MoveValue { get; }
        Vector2 LookValue { get; }
        bool InteractHeld { get; }
        bool MouseHeld { get; }

        void OnEnable();
        void OnDisable();
    }
}
