using UnityEngine;

namespace TimeStranded.Dialogues.Test
{
    /// <summary>
	/// Used to test dialogues.
	/// </summary>
    [AddComponentMenu("Time Stranded/Dialogues/Test/Dialogue Test")]
    [DisallowMultipleComponent]
    public class DialogueTest : MonoBehaviour
    {
        /// <summary>
        /// The dialogue manager to test.
        /// </summary>
        [Tooltip("The dialogue manager to test.")]
        [SerializeField] private DialogueManagerSO _dialogueManager = null;

        /// <summary>
        /// The dialogue to test.
        /// </summary>
        [Tooltip("The dialogue to test.")]
        [SerializeField] private DialogueSO _dialogue = null;

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

        private void Start()
        {
            // Subscribe to the dialogue events.
            _onDialogueStartedChannel.OnRaised += OnStarted;
            _onDialogueNextMessageChannel.OnRaised += OnNextMessage;
            _onDialogueNextChoiceChannel.OnRaised += OnNextChoice;
            _onDialogueFinishedChannel.OnRaised += OnFinished;

            // Start the dialogue.
            _dialogueManager.StartDialogue(_dialogue);

            // Continue to the choice node.
            _dialogueManager.ContinueDialogue();

            // Select the second choice.
            _dialogueManager.ContinueDialogue(1);
        }

        /// <summary>
        /// Called when a dialogue is started.
        /// </summary>
        /// <param name="message">The first message.</param>
        private void OnStarted(MessageNodeData message)
        {
            Debug.Log($"Started dialogue with message: {message.Content}");
        }

        /// <summary>
        /// Called when the next message in a dialogue is reached.
        /// </summary>
        /// <param name="message">The next message.</param>
        private void OnNextMessage(MessageNodeData message)
        {
            Debug.Log(message.Content);
        }

        /// <summary>
        /// Called when the next message in a dialogue is reached.
        /// </summary>
        /// <param name="choice">The next choice.</param>
        private void OnNextChoice(ChoiceNodeData choice)
        {
            int choiceCount = choice.Choices.Count;
            Debug.Log($"Continued to choice with {choiceCount} options.");

            for (int i = 0; i < choiceCount; i++)
            {
                // The content of each choice is found in the node that comes after it.
                Debug.Log($"Choice {i + 1}: {_dialogue.GetMessageNode(choice.Choices[i]).Content}");
            }
        }

        /// <summary>
        /// Called when a dialogue is finished.
        /// </summary>
        /// <param name="message">The last message.</param>
        private void OnFinished(MessageNodeData message)
        {
            Debug.Log($"Finished dialogue with message: {message.Content}");
        }
    }
}
