using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="MessageNodeData"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMessageEventChannel", menuName = "Time Stranded/Dialogues/Message Event Channel")]
    public class MessageEventChannelSO : EventChannelSO<MessageNodeData> { }
}
