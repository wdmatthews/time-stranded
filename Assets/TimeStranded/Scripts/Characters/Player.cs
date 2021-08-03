using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Provides controls for a player.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Player")]
    [DisallowMultipleComponent]
    public class Player : Character
    {
        /// <summary>
        /// A reference to the camera.
        /// </summary>
        [Tooltip("A reference to the camera.")]
        [SerializeField] private CameraReferenceSO _cameraReference = null;

        /// <summary>
        /// The function called when receiving the "Move" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnMove(InputAction.CallbackContext context)
        {
            Move(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// The function called when receiving the "Aim" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnAim(InputAction.CallbackContext context)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            // Don't aim if not receiving aim input.
            if (Mathf.Approximately(direction.x, 0) && Mathf.Approximately(direction.y, 0)) return;

            // If the aim input comes from moving the mouse, aim the player towards the mouse.
            if (context.control.path == "/Mouse/delta")
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Vector3 mouseWorldPosition = _cameraReference.Camera.ScreenToWorldPoint(mouseScreenPosition);
                Aim(mouseWorldPosition - transform.position);
            }
            // Otherwise, aim the player in the input direction, such as a Gamepad's analog stick direction.
            else Aim(direction);
        }

        /// <summary>
        /// The function called when receiving the "Use" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.performed) UseItem();
        }

        /// <summary>
        /// The function called when receiving the "Select" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnSelect(InputAction.CallbackContext context)
        {
            SelectAbility(context.ReadValue<Vector2>());
        }
    }
}
