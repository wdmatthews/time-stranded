using UnityEngine;
using Toolkits.EventSystem;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A component that allows for choice events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Dialogues/Choice Event Listener")]
    public class ChoiceEventListener : EventListener<ChoiceNodeData> { }
}
