using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Input
{
    public class InputReader : MonoBehaviour, Controls.IPlayerActions
    {
        public event System.Action JumpEvent;
        public event System.Action BlockEvent;
        public event System.Action RunEvent;
        public event System.Action TargetEvent;
        public event System.Action CancelEvent;

        public Vector2 movementValue { get; private set; }

        private Controls controls;

        private void Start()
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);

            controls.Player.Enable();
        }

        private void OnDestroy()
        {
            controls.Player.Disable();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            JumpEvent?.Invoke();
        }

        public void OnBlock(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            BlockEvent?.Invoke();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            movementValue = context.ReadValue<Vector2>();
        }

        public void OnRun(InputAction.CallbackContext context)
        {
            RunEvent?.Invoke();
        }

        public void OnLook(InputAction.CallbackContext context)
        {

        }

        public void OnTarget(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            TargetEvent?.Invoke();
        }

        public void OnCancel(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            CancelEvent?.Invoke();
        }
    }
}
