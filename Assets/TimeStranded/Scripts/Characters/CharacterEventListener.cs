using UnityEngine;
using UnityEngine.Events;

namespace TimeStranded.Characters
{
    /// <summary>
    /// A component that allows for character events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Character Event Listener")]
    public class CharacterEventListener : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private CharacterEventChannelSO _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<Character> _onRaised = null;

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
        /// <param name="character">The character to pass when raising the event.</param>
        private void Raise(Character character) => _onRaised?.Invoke(character);
    }
}
