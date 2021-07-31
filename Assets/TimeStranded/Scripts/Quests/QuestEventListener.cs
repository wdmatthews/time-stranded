using UnityEngine;
using UnityEngine.Events;

namespace TimeStranded.Quests
{
    /// <summary>
    /// A component that allows for quest events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Quests/Quest Event Listener")]
    public class QuestEventListener : MonoBehaviour
    {
        /// <summary>
        /// The channel to listen to events from.
        /// </summary>
        [Tooltip("The channel to listen to events from.")]
        [SerializeField] private QuestEventChannelSO _channel = null;

        /// <summary>
        /// The <see cref="UnityEvent"/> to invoke when an event is raised in the channel.
        /// </summary>
        [Tooltip("The UnityEvent to invoke when an event is raised in the channel.")]
        [SerializeField] private UnityEvent<QuestSO> _onRaised = null;

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
        /// <param name="quest">The quest to pass when raising the event.</param>
        private void Raise(QuestSO quest) => _onRaised?.Invoke(quest);
    }
}
