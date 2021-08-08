using UnityEngine;
using UnityEngine.Events;

namespace TimeStranded
{
    /// <summary>
    /// A trigger that invokes UnityEvents.
    /// </summary>
    [AddComponentMenu("Time Stranded/Unity Event Trigger")]
    public class UnityEventTrigger : Trigger
    {
        /// <summary>
        /// The action on enter.
        /// </summary>
        [Tooltip("The action on enter.")]
        [SerializeField] private UnityEvent _onEnter = null;

        /// <summary>
        /// The action on exit.
        /// </summary>
        [Tooltip("The action on exit.")]
        [SerializeField] private UnityEvent _onExit = null;

        /// <summary>
        /// Invokes the UnityEvent for OnEnter.
        /// </summary>
        /// <param name="collider"></param>
        protected override void OnEnter(Collider2D collider) => _onEnter.Invoke();

        /// <summary>
        /// Invokes the UnityEvent for OnExit.
        /// </summary>
        /// <param name="collider"></param>
        protected override void OnExit(Collider2D collider) => _onExit.Invoke();
    }
}
