using UnityEngine;

namespace TimeStranded.Quests
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="QuestSO"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewQuestEventChannel", menuName = "Time Stranded/Quests/Quest Event Channel")]
    public class QuestEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<QuestSO> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="quest">The quest to pass when raising the event.</param>
        public void Raise(QuestSO quest) => OnRaised?.Invoke(quest);
    }
}
