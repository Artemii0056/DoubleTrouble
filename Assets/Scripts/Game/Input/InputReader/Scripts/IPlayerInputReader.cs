using System;
using UnityEngine;

namespace Input.InputReader.Scripts
{
    public interface IPlayerInputReader
    {
        Vector2 MoveValue { get; }
        Vector2 LookValue { get; }
        bool InteractHeld { get; }
        bool MouseHeld { get; }
        event Action InteractStarted;
        event Action InteractCanceled;
        void OnEnable();
        void OnDisable();
    }
}