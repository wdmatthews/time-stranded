using UnityEngine;
using TimeStranded.Characters;
using TimeStranded.Dialogues;

namespace TimeStranded.Management
{
    /// <summary>
    /// Provides basic data and methods needed across most locations.
    /// </summary>
    public abstract class LocationVersion : MonoBehaviour
    {
        /// <summary>
        /// The player's data.
        /// </summary>
        [Tooltip("The player's data.")]
        [SerializeField] protected CharacterSO _playerData = null;

        /// <summary>
        /// The camera follow targets.
        /// </summary>
        [Tooltip("The camera follow targets.")]
        [SerializeField] private TransformListReferenceSO _cameraFollowTargets = null;

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
        /// The player instance.
        /// </summary>
        protected Player _player = null;

        private void Awake()
        {
            // Spawn the player.
            _player = (Player)Instantiate(_playerData.Prefab, transform);
            _player.Initialize(_playerData);
            _player.transform.parent = null;
            _cameraFollowTargets.Add(_player.transform);

            // Subscribe to the dialogue events for the player.
            if (_onDialogueStartedChannel) _onDialogueStartedChannel.OnRaised += OnDialogueStarted;
            if (_onDialogueNextMessageChannel) _onDialogueNextMessageChannel.OnRaised += OnDialogueNextMessage;
            if (_onDialogueNextChoiceChannel) _onDialogueNextChoiceChannel.OnRaised += OnDialogueNextChoice;
            if (_onDialogueFinishedChannel) _onDialogueFinishedChannel.OnRaised += OnDialogueFinished;
        }

        private void OnDestroy()
        {
            if (_onDialogueStartedChannel) _onDialogueStartedChannel.OnRaised -= OnDialogueStarted;
            if (_onDialogueNextMessageChannel) _onDialogueNextMessageChannel.OnRaised -= OnDialogueNextMessage;
            if (_onDialogueNextChoiceChannel) _onDialogueNextChoiceChannel.OnRaised -= OnDialogueNextChoice;
            if (_onDialogueFinishedChannel) _onDialogueFinishedChannel.OnRaised -= OnDialogueFinished;
        }

        /// <summary>
        /// Called when a dialogue is started.
        /// </summary>
        /// <param name="message">The first message.</param>
        private void OnDialogueStarted(MessageNodeData message)
        {
            _player.Move(new Vector2());
            _player.IsInDialogue = true;
            _player.CurrentIsMessage = true;
        }

        /// <summary>
        /// Called when a dialogue is continued to a message.
        /// </summary>
        /// <param name="message">The next message.</param>
        private void OnDialogueNextMessage(MessageNodeData message)
        {
            _player.CurrentIsMessage = true;
        }

        /// <summary>
        /// Called when a dialogue is continued to a choice.
        /// </summary>
        /// <param name="choice">The next choice.</param>
        private void OnDialogueNextChoice(ChoiceNodeData choice)
        {
            _player.CurrentIsMessage = false;
        }

        /// <summary>
        /// Called when a dialogue is finished.
        /// </summary>
        /// <param name="message">The last message.</param>
        private void OnDialogueFinished(MessageNodeData message)
        {
            _player.IsInDialogue = false;
        }
    }
}
