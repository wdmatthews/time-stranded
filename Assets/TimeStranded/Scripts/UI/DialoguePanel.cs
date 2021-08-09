using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Toolkits.Variables;
using TimeStranded.Characters;
using TimeStranded.Dialogues;

namespace TimeStranded.UI
{
    /// <summary>
    /// Shows character and controls dialogue.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Dialogue Panel")]
    [DisallowMultipleComponent]
    public class DialoguePanel : MonoBehaviour
    {
        /// <summary>
        /// The dialogue panel's rect transform.
        /// </summary>
        [Tooltip("The dialogue panel's rect transform.")]
        [SerializeField] private RectTransform _rectTransform = null;

        /// <summary>
        /// The panel's image.
        /// </summary>
        [Tooltip("The panel's image.")]
        [SerializeField] private Image _image = null;

        /// <summary>
        /// The container for the speaker information.
        /// </summary>
        [Tooltip("The container for the speaker information.")]
        [SerializeField] private Transform _speakerContainer = null;

        /// <summary>
        /// The speaker's fill image.
        /// </summary>
        [Tooltip("The speaker's fill image.")]
        [SerializeField] private Image _speakerFill = null;

        /// <summary>
        /// The speaker's face.
        /// </summary>
        [Tooltip("The speaker's face.")]
        [SerializeField] private Image _speakerFace = null;

        /// <summary>
        /// The speaker's name.
        /// </summary>
        [Tooltip("The speaker's name.")]
        [SerializeField] private TextMeshProUGUI _speakerName = null;

        /// <summary>
        /// The message label.
        /// </summary>
        [Tooltip("The message label.")]
        [SerializeField] private TextMeshProUGUI _messageLabel = null;

        /// <summary>
        /// The container for the choices.
        /// </summary>
        [Tooltip("The container for the choices.")]
        [SerializeField] private Transform _choicesContainer = null;

        /// <summary>
        /// The choice labels.
        /// </summary>
        [Tooltip("The choice labels.")]
        [SerializeField] private TextMeshProUGUI[] _choiceLabels = { };

        /// <summary>
        /// The duration of the open/close animation.
        /// </summary>
        [Tooltip("The duration of the open/close animation.")]
        [SerializeField] private float _openCloseAnimationDuration = 1;

        /// <summary>
        /// The panel's bottom margin.
        /// </summary>
        [Tooltip("The panel's bottom margin.")]
        [SerializeField] private float _bottomMargin = 16;

        /// <summary>
        /// The player's name.
        /// </summary>
        [Tooltip("The player's name.")]
        [SerializeField] private StringVariableSO _playerName = null;

        /// <summary>
        /// The player's face.
        /// </summary>
        [Tooltip("The player's face.")]
        [SerializeField] private StringVariableSO _playerFace = null;

        /// <summary>
        /// The player's color.
        /// </summary>
        [Tooltip("The player's color.")]
        [SerializeField] private StringVariableSO _playerColor = null;

        /// <summary>
        /// The collection of character faces.
        /// </summary>
        [Tooltip("The collection of character faces.")]
        [SerializeField] private CharacterFacesSO _faces = null;

        /// <summary>
        /// The collection of character colors.
        /// </summary>
        [Tooltip("The collection of character colors.")]
        [SerializeField] private CharacterColorsSO _colors = null;

        /// <summary>
        /// The dialogue manager.
        /// </summary>
        [Tooltip("The dialogue manager.")]
        [SerializeField] private DialogueManagerSO _dialogueManager = null;

        /// <summary>
        /// The player's data.
        /// </summary>
        [Tooltip("The player's data.")]
        [SerializeField] private CharacterSO _playerData = null;

        /// <summary>
        /// The channel to raise when applying a theme.
        /// </summary>
        [Tooltip("The channel to raise when applying a theme.")]
        [SerializeField] private ThemeEventChannelSO _onThemeApplyChannel = null;

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

        private void Awake()
        {
            if (_onDialogueStartedChannel) _onDialogueStartedChannel.OnRaised += OnDialogueStarted;
            if (_onDialogueNextMessageChannel) _onDialogueNextMessageChannel.OnRaised += OnDialogueNextMessage;
            if (_onDialogueNextChoiceChannel) _onDialogueNextChoiceChannel.OnRaised += OnDialogueNextChoice;
            if (_onDialogueFinishedChannel) _onDialogueFinishedChannel.OnRaised += OnDialogueFinished;
            if (_onThemeApplyChannel) _onThemeApplyChannel.OnRaised += ApplyTheme;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_onDialogueStartedChannel) _onDialogueStartedChannel.OnRaised -= OnDialogueStarted;
            if (_onDialogueNextMessageChannel) _onDialogueNextMessageChannel.OnRaised -= OnDialogueNextMessage;
            if (_onDialogueNextChoiceChannel) _onDialogueNextChoiceChannel.OnRaised -= OnDialogueNextChoice;
            if (_onDialogueFinishedChannel) _onDialogueFinishedChannel.OnRaised -= OnDialogueFinished;
            if (_onThemeApplyChannel) _onThemeApplyChannel.OnRaised -= ApplyTheme;
        }

        /// <summary>
        /// Applies the given theme to the panel.
        /// </summary>
        /// <param name="theme">The theme to apply.</param>
        public void ApplyTheme(ThemeSO theme)
        {
            _image.sprite = theme.ButtonSpritePressed;
        }

        /// <summary>
        /// Called when a dialogue is started.
        /// </summary>
        /// <param name="message">The first message.</param>
        private void OnDialogueStarted(MessageNodeData message)
        {
            gameObject.SetActive(true);
            OnDialogueNextMessage(message);
            _rectTransform.DOAnchorPosY(_rectTransform.rect.height + _bottomMargin, _openCloseAnimationDuration)
                .From(new Vector2());
        }

        /// <summary>
        /// Called when a dialogue is continued to a message.
        /// </summary>
        /// <param name="message">The next message.</param>
        private void OnDialogueNextMessage(MessageNodeData message)
        {
            _messageLabel.text = message.FormattedContent;
            bool speakerIsPlayer = message.Speaker == _playerData;
            _speakerName.text = speakerIsPlayer ? _playerName.Value : message.Speaker.name;
            _speakerFace.sprite = speakerIsPlayer ? _faces[_playerFace.Value].Sprite : message.Speaker.Face.Sprite;
            _speakerFill.color = speakerIsPlayer ? _colors[_playerColor.Value].Color : message.Speaker.Color.Color;
            _speakerContainer.gameObject.SetActive(true);
            _messageLabel.gameObject.SetActive(true);
            _choicesContainer.gameObject.SetActive(false);
        }

        /// <summary>
        /// Called when a dialogue is continued to a choice.
        /// </summary>
        /// <param name="choice">The next choice.</param>
        private void OnDialogueNextChoice(ChoiceNodeData choice)
        {
            int maxChoiceCount = _choiceLabels.Length;
            int choiceCount = choice.Choices.Count;

            for (int i = 0; i < maxChoiceCount; i++)
            {
                TextMeshProUGUI choiceLabel = _choiceLabels[i];
                choiceLabel.gameObject.SetActive(i < choiceCount);

                if (i < choiceCount)
                {
                    choiceLabel.text = _dialogueManager.CurrentDialogue
                        .GetMessageNode(choice.Choices[i]).FormattedContent;
                }
            }

            _speakerContainer.gameObject.SetActive(false);
            _messageLabel.gameObject.SetActive(false);
            _choicesContainer.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called when a dialogue is finished.
        /// </summary>
        /// <param name="message">The last message.</param>
        private void OnDialogueFinished(MessageNodeData message)
        {
            _rectTransform.DOAnchorPosY(0, _openCloseAnimationDuration)
                .From(new Vector2(0, _rectTransform.rect.height + _bottomMargin))
                .onComplete += Hide;
        }

        /// <summary>
        /// Hides the panel.
        /// </summary>
        private void Hide() => gameObject.SetActive(false);
    }
}
