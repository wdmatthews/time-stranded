using UnityEngine;

namespace Toolkits.Events.Test
{
    /// <summary>
    /// Used to test object pooling.
    /// </summary>
    [AddComponentMenu("Toolkits/Events/Test/Test")]
    [DisallowMultipleComponent]
    public class EventsTest : MonoBehaviour
    {
        /// <summary>
        /// The event channel to raise a test event in.
        /// </summary>
        [Tooltip("The event channel to raise a test event in.")]
        [SerializeField] private EventChannelSO _channel = null;

        /// <summary>
        /// The event listener to disable.
        /// </summary>
        [Tooltip("The event listener to disable.")]
        [SerializeField] private EventListener _listener = null;

        /// <summary>
        /// The camera to toggle during testing.
        /// </summary>
        [Tooltip("The camera to toggle during testing.")]
        [SerializeField] private Camera _camera = null;

        private void Start()
        {
            // Raise the event.
            _channel.Raise();
            Debug.Log("Camera turned off by the event.");

            // Turn the camera back on.
            _camera.gameObject.SetActive(true);
            Debug.Log("Camera turned on.");

            // Disable the event listener.
            _listener.gameObject.SetActive(false);
            Debug.Log("Listener turned off.");

            // Raise the event again.
            _channel.Raise();
            Debug.Log("Event raised and the camera stayed on.");
        }
    }
}
