using UnityEngine;
using UnityEngine.Events;

namespace Toolkits.Events
{
    /// <summary>
    /// A component that allows for events to be assigned in the inspector.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public abstract class EventListener<T1> : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private EventChannelSO<T1> _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<T1> _onRaised = null;

        /// <summary>
        /// Start listening to the event channel when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            if (_channel) _channel.OnRaised += Raise;
        }

        /// <summary>
        /// Stop listening to the event channel when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (_channel) _channel.OnRaised -= Raise;
        }

        /// <summary>
        /// Raises the Unity event from the inspector.
        /// </summary>
        /// <param name="parameter1"></param>
        private void Raise(T1 parameter1) => _onRaised?.Invoke(parameter1);
    }
}
