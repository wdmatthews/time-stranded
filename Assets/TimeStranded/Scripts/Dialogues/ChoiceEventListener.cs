using UnityEngine;
using UnityEngine.Events;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A component that allows for choice events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Dialogues/Choice Event Listener")]
    public class ChoiceEventListener : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private ChoiceEventChannelSO _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<ChoiceNodeData> _onRaised = null;

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
        /// <param name="choice">The choice to pass when raising the event.</param>
        private void Raise(ChoiceNodeData choice) => _onRaised?.Invoke(choice);
    }
}
