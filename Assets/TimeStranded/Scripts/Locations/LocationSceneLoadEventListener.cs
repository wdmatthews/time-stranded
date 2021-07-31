using UnityEngine;
using UnityEngine.Events;

namespace TimeStranded.Locations
{
    /// <summary>
    /// A component that allows for location scene load events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Locations/Location Scene Load Event Listener")]
    public class LocationSceneLoadEventListener : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private LocationSceneLoadEventChannelSO _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<LocationSO, float> _onRaised = null;

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
        /// <param name="location">The location to pass when raising the event.</param>
        /// <param name="progress">The progress to pass when raising the event.</param>
        private void Raise(LocationSO location, float progress) => _onRaised?.Invoke(location, progress);
    }
}
