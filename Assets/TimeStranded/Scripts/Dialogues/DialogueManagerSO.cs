using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// Manages the currently active dialogue, if there is one.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogueManager", menuName = "Time Stranded/Dialogues/Dialogue Manager")]
    public class DialogueManagerSO : ScriptableObject
    {
        /// <summary>
        /// The event channel to raise when a dialogue is started.
        /// </summary>
        [Tooltip("The event channel to raise when a dialogue is started.")]
        [SerializeField] private MessageEventChannelSO _onDialogueStartedChannel = null;

        /// <summary>
        /// The event channel to raise when a dialogue moves to the next message.
        /// </summary>
        [Tooltip("The event channel to raise when a dialogue moves to the next message.")]
        [SerializeField] private MessageEventChannelSO _onDialogueNextMessageChannel = null;

        /// <summary>
        /// The event channel to raise when a dialogue moves to the next choice.
        /// </summary>
        [Tooltip("The event channel to raise when a dialogue moves to the next choice.")]
        [SerializeField] private ChoiceEventChannelSO _onDialogueNextChoiceChannel = null;

        /// <summary>
        /// The event channel to raise when a dialogue is finished.
        /// </summary>
        [Tooltip("The event channel to raise when a dialogue is finished.")]
        [SerializeField] private MessageEventChannelSO _onDialogueFinishedChannel = null;

        /// <summary>
        /// The event channel to raise when an dialogue is continued from a message.
        /// </summary>
        [Tooltip("The event channel to raise when an dialogue is continued from a message.")]
        [SerializeField] private EventChannelSO _onDialogueContinueMessage = null;

        /// <summary>
        /// The event channel to raise when an dialogue is continued from a choice.
        /// </summary>
        [Tooltip("The event channel to raise when an dialogue is continued from a choice.")]
        [SerializeField] private IntEventChannelSO _onDialogueContinueChoice = null;

        /// <summary>
        /// The current dialogue.
        /// </summary>
        [System.NonSerialized] public DialogueSO CurrentDialogue = null;

        /// <summary>
        /// The current message or choice guid.
        /// </summary>
        [System.NonSerialized] private string _currentGuid = "";

        /// <summary>
        /// The current message.
        /// </summary>
        [System.NonSerialized] private MessageNodeData _currentMessage = null;

        /// <summary>
        /// The current choice.
        /// </summary>
        [System.NonSerialized] private ChoiceNodeData _currentChoice = null;

        /// <summary>
        /// Whether or not the dialogue reached the end.
        /// </summary>
        [System.NonSerialized] public bool IsFinished = false;

        /// <summary>
        /// Starts the given dialogue.
        /// </summary>
        /// <param name="dialogue">The dialogue to start.</param>
        public void StartDialogue(DialogueSO dialogue)
        {
            // Exit if there is no start message.
            if (dialogue.StartMessageNodeGuid.Length == 0) return;
            CurrentDialogue = dialogue;
            // Start from the first message.
            _currentGuid = CurrentDialogue.StartMessageNodeGuid;
            // Raise the start event with the first message.
            _currentMessage = CurrentDialogue.GetMessageNode(_currentGuid);
            _currentMessage.Event?.Invoke();
            _onDialogueStartedChannel?.Raise(_currentMessage);
            // Subscribe to the needed events.
            _onDialogueContinueMessage.OnRaised += ContinueDialogue;
            _onDialogueContinueChoice.OnRaised += ContinueDialogue;
        }

        /// <summary>
        /// Continues the current dialogue.
        /// </summary>
        /// <param name="choiceIndex">The index of the selected choice, or -1 if the current node is a message node.</param>
        public void ContinueDialogue(int choiceIndex)
        {
            if (!CurrentDialogue) return;
            // Finish the dialogue if needed.
            if (IsFinished)
            {
                FinishDialogue();
                return;
            }

            // Move on from the current message to either a message or a choice.
            if (choiceIndex < 0)
            {
                _currentGuid = _currentMessage.NextId;

                // Get the next message.
                if (_currentMessage.NextIsMessage) GetCurrentMessage();
                // Get the next choice.
                else
                {
                    _currentMessage = null;
                    _currentChoice = CurrentDialogue.GetChoiceNode(_currentGuid);
                    _onDialogueNextChoiceChannel?.Raise(_currentChoice);
                }
            }
            // Move on from the current choice.
            else
            {
                if (choiceIndex >= _currentChoice.Choices.Count) return;
                // Get the next message.
                _currentGuid = _currentChoice.Choices[choiceIndex];
                _currentChoice = null;
                GetCurrentMessage();
            }
        }

        /// <summary>
        /// Continues the current dialogue.
        /// </summary>
        public void ContinueDialogue() => ContinueDialogue(-1);

        /// <summary>
        /// Gets the current message.
        /// </summary>
        private void GetCurrentMessage()
        {
            // Get the message.
            _currentMessage = CurrentDialogue.GetMessageNode(_currentGuid);
            // Trigger its event if it has one.
            _currentMessage.Event?.Invoke();

            // Finish if this message is the last one.
            _onDialogueNextMessageChannel?.Raise(_currentMessage);
            if (_currentMessage.NextId.Length == 0) IsFinished = true;
        }

        /// <summary>
        /// Finishes the dialogue.
        /// </summary>
        private void FinishDialogue()
        {
            _onDialogueFinishedChannel?.Raise(_currentMessage);
            CurrentDialogue = null;
            IsFinished = false;
            _onDialogueContinueMessage.OnRaised -= ContinueDialogue;
            _onDialogueContinueChoice.OnRaised -= ContinueDialogue;
        }
    }
}
