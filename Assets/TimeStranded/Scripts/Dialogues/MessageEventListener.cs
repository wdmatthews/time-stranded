using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A component that allows for message events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Dialogues/Message Event Listener")]
    public class MessageEventListener : EventListener<MessageNodeData> { }
}
