using UnityEngine;
using UnityEngine.Events;

namespace Toolkits.EventSystem
{
    /// <summary>
    /// A component that allows for events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Toolkits/Event System/Event Listener")]
    public class EventListener : MonoBehaviour
    {
        [SerializeField] private EventChannelSO _channel = null;
        [SerializeField] private UnityEvent _onRaised = null;

        /// <summary>
        /// Start observing the event channel when the script is enabled.
        /// </summary>
        private void OnEnable()
        {
            if (_channel) _channel.OnRaised += Raise;
        }

        /// <summary>
        /// Stop observing the event channel when the script is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (_channel) _channel.OnRaised -= Raise;
        }

        /// <summary>
        /// Raises the Unity event from the inspector.
        /// </summary>
        private void Raise() => _onRaised?.Invoke();
    }
}
