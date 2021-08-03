using UnityEngine;

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
        /// The current dialogue.
        /// </summary>
        [System.NonSerialized] private DialogueSO _currentDialogue = null;

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
        /// Starts the given dialogue.
        /// </summary>
        /// <param name="dialogue">The dialogue to start.</param>
        public void StartDialogue(DialogueSO dialogue)
        {
            // Exit if there is no start message.
            if (dialogue.StartMessageNodeGuid.Length == 0) return;
            _currentDialogue = dialogue;
            // Start from the first message.
            _currentGuid = _currentDialogue.StartMessageNodeGuid;
            // Raise the start event with the first message.
            _currentMessage = _currentDialogue.GetMessageNode(_currentGuid);
            _onDialogueStartedChannel.Raise(_currentMessage);
        }

        /// <summary>
        /// Continues the current dialogue.
        /// </summary>
        /// <param name="choiceIndex">The index of the selected choice, or -1 if the current node is a message node.</param>
        public void ContinueDialogue(int choiceIndex = -1)
        {
            if (!_currentDialogue) return;

            // Move on from the current message to either a message or a choice.
            if (choiceIndex < 0)
            {
                _currentGuid = _currentMessage.NextId;

                // Get the next message.
                if (_currentMessage.NextIsMessage)
                {
                    _currentMessage = _currentDialogue.GetMessageNode(_currentGuid);

                    // Finish if this message is the last one.
                    if (_currentMessage.NextId.Length == 0) _onDialogueFinishedChannel.Raise(_currentMessage);
                    else _onDialogueNextMessageChannel.Raise(_currentMessage);
                }
                // Get the next choice.
                else
                {
                    _currentMessage = null;
                    _currentChoice = _currentDialogue.GetChoiceNode(_currentGuid);
                    _onDialogueNextChoiceChannel.Raise(_currentChoice);
                }
            }
            // Move on from the current choice.
            else
            {
                // Get the next message.
                _currentGuid = _currentChoice.Choices[choiceIndex];
                _currentChoice = null;
                _currentMessage = _currentDialogue.GetMessageNode(_currentGuid);

                // Finish if this message is the last one.
                if (_currentMessage.NextId.Length == 0) _onDialogueFinishedChannel.Raise(_currentMessage);
                else _onDialogueNextMessageChannel.Raise(_currentMessage);
            }
        }
    }
}
