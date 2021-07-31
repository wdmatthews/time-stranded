using UnityEngine;

namespace TimeStranded
{
    /// <summary>
    /// A base class that acts when OnTriggerEnter2D and OnTriggerExit2D are called.
    /// </summary>
    public abstract class Trigger : MonoBehaviour
    {
        /// <summary>
        /// The layers that can interact with this trigger.
        /// </summary>
        [Tooltip("The layers that can interact with this trigger.")]
        [SerializeField] protected LayerMask _targetLayers = 0;

        /// <summary>
        /// The trigger's collider.
        /// </summary>
        [Tooltip("The trigger's collider.")]
        [SerializeField] protected Collider2D _collider = null;

        protected void OnTriggerEnter2D(Collider2D collider)
        {
            if (!_collider.IsTouchingLayers(_targetLayers)) return;
            OnEnter(collider);
        }

        /// <summary>
        /// Called when a collider enters the trigger.
        /// </summary>
        /// <param name="collider">The collider that entered.</param>
        protected virtual void OnEnter(Collider2D collider) { }

        protected void OnTriggerExit2D(Collider2D collider)
        {
            if (!_collider.IsTouchingLayers(_targetLayers)) return;
            OnExit(collider);
        }

        /// <summary>
        /// Called when a collider exists the trigger.
        /// </summary>
        /// <param name="collider">The collider that entered.</param>
        protected virtual void OnExit(Collider2D collider) { }
    }
}
