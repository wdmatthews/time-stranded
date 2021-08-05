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
            // If dead, disabled controls.
            if (_isDead) return;
            Move(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// The function called when receiving the "Aim" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnAim(InputAction.CallbackContext context)
        {
            // If dead, disabled controls.
            if (_isDead) return;
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
            // If dead, disabled controls.
            if (_isDead) return;
            if (context.performed) UseItem();
        }

        /// <summary>
        /// The function called when receiving the "Select" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnSelect(InputAction.CallbackContext context)
        {
            // If dead, disabled controls.
            if (_isDead) return;
            Vector2 abilityDirection = context.ReadValue<Vector2>();
            int abilityIndex = -1;
            // Get the ability index based on the vector.
            // Up = 0, Right = 1, Down = 2, Left = 3.
            if (Mathf.Approximately(abilityDirection.y, 1)) abilityIndex = 0;
            else if (Mathf.Approximately(abilityDirection.x, 1)) abilityIndex = 1;
            else if (Mathf.Approximately(abilityDirection.y, -1)) abilityIndex = 2;
            else if (Mathf.Approximately(abilityDirection.x, -1)) abilityIndex = 3;
            if (abilityIndex < 0 || abilityIndex >= _abilities.Count) return;
            SelectAbility(abilityIndex);
        }
    }
}
