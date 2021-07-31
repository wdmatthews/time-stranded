using UnityEngine;

namespace TimeStranded.Quests
{
    /// <summary>
    /// Stores information about a quest.
    /// </summary>
    [CreateAssetMenu(fileName = "NewQuest", menuName = "Time Stranded/Quests/Quest")]
    public class QuestSO : ScriptableObject
    {
        /// <summary>
        /// The max progress for the quest.
        /// </summary>
        [Tooltip("The max progress for the quest.")]
        public int MaxProgress = 1;

        /// <summary>
        /// Whether or not the quest is completed when the max progress is reached.
        /// </summary>
        [Tooltip("Whether or not the quest is completed when the max progress is reached.")]
        public bool AutoComplete = true;

        /// <summary>
        /// The event channel to raise when the quest is completed.
        /// </summary>
        [Tooltip("The event channel to raise when the quest is completed.")]
        [SerializeField] private QuestEventChannelSO _onCompleteChannel = null;

        /// <summary>
        /// The current quest progress.
        /// </summary>
        [System.NonSerialized] public int Progress = 0;

        /// <summary>
        /// Whether or not the quest is ready to complete.
        /// </summary>
        [System.NonSerialized] public bool IsReadyToComplete = false;

        /// <summary>
        /// Whether or not the quest is complete.
        /// </summary>
        [System.NonSerialized] public bool IsComplete = false;

        /// <summary>
        /// Whether or not the quest is active.
        /// </summary>
        [System.NonSerialized] public bool IsActive = false;

        /// <summary>
        /// Changes the quest's progress by the given amount.
        /// </summary>
        /// <param name="amount">The amount to change by.</param>
        private void ChangeProgress(int amount)
        {
            if (IsComplete || !IsActive) return;
            Progress = Mathf.Clamp(Progress + amount, 0, MaxProgress);
            if (!IsReadyToComplete && Progress == MaxProgress && AutoComplete) Complete();
            IsReadyToComplete = Progress == MaxProgress;
        }

        /// <summary>
        /// Increases the quest's progress by the given amount.
        /// </summary>
        /// <param name="amount">The amount to increase by.</param>
        public void IncreaseProgress(int amount) => ChangeProgress(amount);

        /// <summary>
        /// Decreases the quest's progress by the given amount.
        /// </summary>
        /// <param name="amount">The amount to decrease by.</param>
        public void DecreaseProgress(int amount) => ChangeProgress(-amount);

        /// <summary>
        /// Resets the quest's progress.
        /// </summary>
        public void ResetProgress()
        {
            Progress = 0;
            IsReadyToComplete = false;
            IsComplete = false;
        }

        /// <summary>
        /// Marks the quest as complete and raises an event.
        /// </summary>
        public void Complete()
        {
            IsComplete = true;
            _onCompleteChannel.Raise(this);
        }
    }
}
