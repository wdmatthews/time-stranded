using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Quests
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="QuestSO"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewQuestEventChannel", menuName = "Time Stranded/Quests/Quest Event Channel")]
    public class QuestEventChannelSO : EventChannelSO<QuestSO> { }
}
