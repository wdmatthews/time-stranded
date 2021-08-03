using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Quests
{
    /// <summary>
    /// A component that allows for quest events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Quests/Quest Event Listener")]
    public class QuestEventListener : EventListener<QuestSO> { }
}
