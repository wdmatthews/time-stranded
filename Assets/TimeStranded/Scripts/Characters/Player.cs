using UnityEngine;
using UnityEngine.InputSystem;
using Toolkits.Events;
using TimeStranded.Cameras;

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
        /// The event channel to raise when an interactable is in range.
        /// </summary>
        [Tooltip("The event channel to raise when an interactable is in range.")]
        [SerializeField] private InteractableEventChannelSO _onInteractableInRangeChannel = null;

        /// <summary>
        /// The event channel to raise when an dialogue is continued from a message.
        /// </summary>
        [Tooltip("The event channel to raise when an dialogue is continued from a message.")]
        [SerializeField] private EventChannelSO _onDialogueContinueMessage = null;

        /// <summary>
        /// The event channel to raise when an dialogue is continued from a choice.
        /// </summary>
        [Tooltip("The event channel to raise when an dialogue is continued from a choice.")]
        [SerializeField] private IntEventChannelSO _onDialogueContinueChoice = null;

        /// <summary>
        /// The current interactable.
        /// </summary>
        [System.NonSerialized] private Interactable _interactable = null;

        /// <summary>
        /// Whether or not the player is in a dialogue.
        /// </summary>
        [System.NonSerialized] public bool IsInDialogue = false;

        /// <summary>
        /// Whether or not the current dialogue node is a message.
        /// </summary>
        [System.NonSerialized] public bool CurrentIsMessage = true;

        protected override void Awake()
        {
            base.Awake();
            if (_onInteractableInRangeChannel) _onInteractableInRangeChannel.OnRaised += SetInteractable;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            if (_onInteractableInRangeChannel) _onInteractableInRangeChannel.OnRaised -= SetInteractable;
        }

        /// <summary>
        /// Sets the current interactable.
        /// </summary>
        /// <param name="interactable">The new interactable</param>
        private void SetInteractable(Interactable interactable)
        {
            _interactable = interactable;
        }

        /// <summary>
        /// The function called when receiving the "Move" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnMove(InputAction.CallbackContext context)
        {
            // If dead, in a dialogue or paused, disable controls.
            if (_isDead || IsInDialogue || _isPaused) return;
            Move(context.ReadValue<Vector2>());
        }

        /// <summary>
        /// The function called when receiving the "Aim" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnAim(InputAction.CallbackContext context)
        {
            // If dead, in a dialogue, or paused, disable controls.
            if (_isDead || IsInDialogue || _isPaused) return;
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
            // If dead, in a dialogue, or paused, disable controls.
            if (_isDead || IsInDialogue || _isPaused) return;
            if (context.performed) UseItem();
        }

        /// <summary>
        /// The function called when receiving the "Select" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnSelect(InputAction.CallbackContext context)
        {
            // If dead or paused, disable controls.
            if (_isDead || _isPaused) return;
            Vector2 abilityDirection = context.ReadValue<Vector2>();
            int abilityIndex = -1;
            // Get the ability index based on the vector.
            // Up = 0, Right = 1, Down = 2, Left = 3.
            if (Mathf.Approximately(abilityDirection.y, 1)) abilityIndex = 0;
            else if (Mathf.Approximately(abilityDirection.x, 1)) abilityIndex = 1;
            else if (Mathf.Approximately(abilityDirection.y, -1)) abilityIndex = 2;
            else if (Mathf.Approximately(abilityDirection.x, -1)) abilityIndex = 3;
            if (abilityIndex < 0 || !IsInDialogue && abilityIndex >= _abilities.Count) return;

            // If in dialogue try to continue.
            if (IsInDialogue && !CurrentIsMessage) _onDialogueContinueChoice.Raise(abilityIndex);
            else if (!IsInDialogue) SelectAbility(abilityIndex);
        }

        /// <summary>
        /// The function called when receiving the "Interact" input.
        /// </summary>
        /// <param name="context">The input context.</param>
        public void OnInteract(InputAction.CallbackContext context)
        {
            if (_isPaused) return;
            // Continue the dialogue.
            if (context.performed && IsInDialogue && CurrentIsMessage) _onDialogueContinueMessage.Raise();
            // Attempt to interact with the current interactable.
            else if (context.performed && !IsInDialogue && _interactable && _interactable.InteractorInRange)
            {
                _interactable.OnInteract.Invoke();
            }
        }
    }
}
