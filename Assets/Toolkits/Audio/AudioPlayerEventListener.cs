using UnityEngine;
using UnityEngine.Events;

namespace Toolkits.Audio
{
    /// <summary>
    /// A component that allows for audio player events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Toolkits/Audio/Audio Player Event Listener")]
    public class AudioPlayerEventListener : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private AudioPlayerEventChannelSO _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<AudioPlayer> _onRaised = null;

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
        private void Raise(AudioPlayer audioPlayer) => _onRaised?.Invoke(audioPlayer);
    }
}
