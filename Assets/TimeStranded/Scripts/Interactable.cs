using UnityEngine;
using UnityEngine.Events;

namespace TimeStranded
{
    /// <summary>
    /// Something or someone that can be interacted with.
    /// </summary>
    [AddComponentMenu("Time Stranded/Interactable")]
    [DisallowMultipleComponent]
    public class Interactable : Trigger
    {
        /// <summary>
        /// The event to raise when interacting.
        /// </summary>
        [Tooltip("The event to raise when interacting.")]
        public UnityEvent OnInteract = null;

        /// <summary>
        /// The event channel to raise when an interactable is in range.
        /// </summary>
        [Tooltip("The event channel to raise when an interactable is in range.")]
        [SerializeField] private InteractableEventChannelSO _onInteractableInRangeChannel = null;

        /// <summary>
        /// Whether or not the interactor is in range.
        /// </summary>
        [System.NonSerialized] public bool InteractorInRange = false;

        /// <summary>
        /// The interactor is in range.
        /// </summary>
        /// <param name="collider"></param>
        protected override void OnEnter(Collider2D collider)
        {
            InteractorInRange = true;
            _onInteractableInRangeChannel?.Raise(this);
        }

        /// <summary>
        /// The interactor is out of range.
        /// </summary>
        /// <param name="collider"></param>
        protected override void OnExit(Collider2D collider)
        {
            InteractorInRange = false;
        }
    }
}
