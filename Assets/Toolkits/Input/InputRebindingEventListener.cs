using UnityEngine;
using UnityEngine.Events;

namespace Toolkits.Input
{
    /// <summary>
    /// A component that allows for input rebinding events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Toolkits/Input/Input Rebinding Event Listener")]
    public class InputRebindingEventListener : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private InputRebindingEventChannelSO _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<SavedInputRebinding> _onRaised = null;

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
        /// <param name="inputRebinding">The input rebinding to pass when raising the event.</param>
        private void Raise(SavedInputRebinding inputRebinding) => _onRaised?.Invoke(inputRebinding);
    }
}
