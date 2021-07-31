using UnityEngine;

namespace TimeStranded.Quests.Test
{
    /// <summary>
	/// Used to test changing a quest's progress.
	/// </summary>
    [AddComponentMenu("Time Stranded/Quests/Test/Quest Progress Test")]
    [DisallowMultipleComponent]
    public class QuestProgressTest : MonoBehaviour
    {
        /// <summary>
        /// The event channel to raise when the quest is completed.
        /// </summary>
        [Tooltip("The event channel to raise when the quest is completed.")]
        [SerializeField] private QuestEventChannelSO _onCompleteChannel = null;

        private void Start()
        {
            // Subscribe to the OnComplete event.
            _onCompleteChannel.OnRaised += OnComplete;
        }

        /// <summary>
        /// Called when the quest is complete.
        /// </summary>
        /// <param name="quest">The completed quest.</param>
        private void OnComplete(QuestSO quest)
        {
            Debug.Log($"Completed quest: {quest.name}");
        }
    }
}
