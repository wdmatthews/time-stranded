using UnityEngine;
using UnityEngine.InputSystem;

namespace TimeStranded
{
    /// <summary>
    /// Provides controls for a player.
    /// </summary>
    [AddComponentMenu("Time Stranded/Player")]
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
            if (Mathf.Approximately(direction.x, 0) && Mathf.Approximately(direction.y, 0)) return;

            if (context.control.path == "/Mouse/delta")
            {
                Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
                Vector3 mouseWorldPosition = _cameraReference.Camera.ScreenToWorldPoint(mouseScreenPosition);
                Aim(mouseWorldPosition - transform.position);
            }
            else Aim(direction);
        }
    }
}
