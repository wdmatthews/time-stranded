using UnityEngine;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="MessageNodeData"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMessageEventChannel", menuName = "Time Stranded/Dialogues/Message Event Channel")]
    public class MessageEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<MessageNodeData> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="message">The message to pass when raising the event.</param>
        public void Raise(MessageNodeData message) => OnRaised?.Invoke(message);
    }
}
