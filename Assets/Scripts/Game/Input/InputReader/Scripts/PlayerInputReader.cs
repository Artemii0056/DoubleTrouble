using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.InputReader.Scripts
{
    public class PlayerInputReader : IPlayerInputReader
    {
        public event Action InteractStarted;
        public event Action InteractCanceled;

        private readonly PlayerInputActions _input;

        public PlayerInputReader()
        {
            _input = new PlayerInputActions();
        }
        
        public Vector2 MoveValue { get; private set; }
        public Vector2 LookValue { get; private set; }

        public bool InteractHeld { get; private set; }
        public bool MouseHeld { get; private set; }

        public void OnEnable()
        {
            _input.Enable();

            _input.Player.Move.performed += OnMovePerformed;
            _input.Player.Move.canceled += OnMoveCanceled;

            _input.Player.Look.performed += OnLookPerformed;
            _input.Player.Look.canceled += OnLookCanceled;

            _input.Player.Interact.started += OnInteractStarted;
            _input.Player.Interact.canceled += OnInteractCanceled;
            
            _input.Player.MouseHold.started += OnMouseStarted;
            _input.Player.MouseHold.canceled += OnMouseCanceled;
        }

        public void OnDisable()
        {
            _input.Player.Move.performed -= OnMovePerformed;
            _input.Player.Move.canceled -= OnMoveCanceled;

            _input.Player.Look.performed -= OnLookPerformed;
            _input.Player.Look.canceled -= OnLookCanceled;

            _input.Player.Interact.started -= OnInteractStarted;
            _input.Player.Interact.canceled -= OnInteractCanceled;
            
            _input.Player.MouseHold.started -= OnMouseStarted;
            _input.Player.MouseHold.canceled -= OnMouseCanceled;

            _input.Disable();
        }

        private void OnMovePerformed(InputAction.CallbackContext context)
        {
            MoveValue = context.ReadValue<Vector2>();
        }
        
        private void OnMouseStarted(InputAction.CallbackContext context)
        {
            MouseHeld = true;
        }
        
        private void OnMouseCanceled(InputAction.CallbackContext context)
        {
            MouseHeld = false;
        }

        private void OnMoveCanceled(InputAction.CallbackContext context)
        {
            MoveValue = Vector2.zero;
        }

        private void OnLookPerformed(InputAction.CallbackContext context)
        {
            LookValue = context.ReadValue<Vector2>();
        }

        private void OnLookCanceled(InputAction.CallbackContext context)
        {
            LookValue = Vector2.zero;
        }

        private void OnInteractStarted(InputAction.CallbackContext context)
        {
            InteractHeld = true;
            InteractStarted?.Invoke();
        }

        private void OnInteractCanceled(InputAction.CallbackContext context)
        {
            InteractHeld = false;
            InteractCanceled?.Invoke();
        }
    }
}
