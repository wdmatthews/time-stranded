using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Toolkits.Events;
using Toolkits.Variables;
using TimeStranded.Characters;

namespace TimeStranded.UI
{
    /// <summary>
    /// Controls the main menu.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Main Menu")]
    [DisallowMultipleComponent]
    public class MainMenu : MonoBehaviour
    {
        /// <summary>
        /// The title screen.
        /// </summary>
        [Tooltip("The title screen.")]
        [SerializeField] private Transform _titleScreen = null;

        /// <summary>
        /// The other screens.
        /// </summary>
        [Tooltip("The other screens.")]
        [SerializeField] private Transform[] _otherScreens = null;

        /// <summary>
        /// The event channel to raise when showing the screen transition.
        /// </summary>
        [Tooltip("The event channel to raise when showing the screen transition.")]
        [SerializeField] private IntEventChannelSO _onScreenTransitionShowChannel = null;

        /// <summary>
        /// The event channel to raise when the screen transition finishes.
        /// </summary>
        [Tooltip("The event channel to raise when the screen transition finishes.")]
        [SerializeField] private EventChannelSO _onScreenTransitionFinishChannel = null;

        /// <summary>
        /// The radio button group for storyline options.
        /// </summary>
        [Tooltip("The radio button group for storyline options.")]
        [SerializeField] private RadioButtonGroup _newGameStorylineOptionGroup = null;

        /// <summary>
        /// The radio button group for player face options.
        /// </summary>
        [Tooltip("The radio button group for player face options.")]
        [SerializeField] private RadioButtonGroup _newGamePlayerFaceOptionGroup = null;

        /// <summary>
        /// The radio button group for player color options.
        /// </summary>
        [Tooltip("The radio button group for player color options.")]
        [SerializeField] private RadioButtonGroup _newGamePlayerColorOptionGroup = null;

        /// <summary>
        /// The player new game fill image.
        /// </summary>
        [Tooltip("The player new game fill image.")]
        [SerializeField] private Image _newGamePlayerFillImage = null;

        /// <summary>
        /// The player new game face image.
        /// </summary>
        [Tooltip("The player new game face image.")]
        [SerializeField] private Image _newGamePlayerFaceImage = null;

        /// <summary>
        /// The button that starts a new game.
        /// </summary>
        [Tooltip("The button that starts a new game.")]
        [SerializeField] private Button _newGameStartButton = null;

        /// <summary>
        /// A list of character faces.
        /// </summary>
        [Tooltip("A list of character faces.")]
        [SerializeField] private CharacterFacesSO _characterFaces = null;

        /// <summary>
        /// A list of character colors.
        /// </summary>
        [Tooltip("A list of character colors.")]
        [SerializeField] private CharacterColorsSO _characterColors = null;

        /// <summary>
        /// The selected new game's storyline.
        /// </summary>
        [SerializeField] private StringVariableSO _storyline = null;

        /// <summary>
        /// The name of the new game's town.
        /// </summary>
        [SerializeField] private StringVariableSO _townName = null;

        /// <summary>
        /// The name of the new game's player.
        /// </summary>
        [SerializeField] private StringVariableSO _playerName = null;

        /// <summary>
        /// The selected new game's player's face.
        /// </summary>
        [SerializeField] private StringVariableSO _playerFace = null;

        /// <summary>
        /// The selected new game's player's color.
        /// </summary>
        [SerializeField] private StringVariableSO _playerColor = null;

        /// <summary>
        /// The main menu's current screen.
        /// </summary>
        private Transform _currentScreen = null;

        /// <summary>
        /// The main menu's current screen.
        /// </summary>
        private Transform _nextScreen = null;

        /// <summary>
        /// The current transition stage.
        /// </summary>
        private int _transitionStage = 0;

        private void Awake()
        {
            _currentScreen = _titleScreen;
            _titleScreen.gameObject.SetActive(true);

            for (int i = _otherScreens.Length - 1; i >= 0; i--)
            {
                _otherScreens[i].gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            _newGameStorylineOptionGroup.Initialize(new List<string> { "Future", "Past" });
            List<string> faceNames = new List<string>();
            List<Sprite> faceSprites = new List<Sprite>();
            int faceCount = _characterFaces.Collection.Length;

            for (int i = 0; i < faceCount; i++)
            {
                CharacterFaceSO face = _characterFaces.Collection[i];
                faceNames.Add(face.name);
                faceSprites.Add(face.Sprite);
            }

            _newGamePlayerFaceOptionGroup.Initialize(faceNames, faceSprites);
            List<string> colorNames = new List<string>();
            List<Color> colors = new List<Color>();
            int colorCount = _characterColors.Collection.Length;

            for (int i = 0; i < colorCount; i++)
            {
                CharacterColorSO color = _characterColors.Collection[i];
                colorNames.Add(color.name);
                colors.Add(color.Color);
            }

            _newGamePlayerColorOptionGroup.Initialize(colorNames, null, colors);
        }

        /// <summary>
        /// Opens the given screen.
        /// </summary>
        /// <param name="screen">The screen to open.</param>
        public void OpenScreen(Transform screen)
        {
            _nextScreen = screen;
            _onScreenTransitionFinishChannel.OnRaised += OnTransitionFinish;
            _onScreenTransitionShowChannel.Raise(0);
            _transitionStage = 0;
        }

        /// <summary>
        /// Transition screens if needed.
        /// </summary>
        private void OnTransitionFinish()
        {
            _onScreenTransitionFinishChannel.OnRaised -= OnTransitionFinish;

            // Turn off the old screen and turn on the new one.
            if (_transitionStage == 0)
            {
                _transitionStage++;
                _nextScreen.gameObject.SetActive(true);
                _currentScreen.gameObject.SetActive(false);
                _currentScreen = _nextScreen;
                _nextScreen = null;

                // Close the transition screen.
                _onScreenTransitionFinishChannel.OnRaised += OnTransitionFinish;
                _onScreenTransitionShowChannel.Raise(1);
            }
        }

        /// <summary>
        /// Validates the player and town name for a new game.
        /// </summary>
        public void NewGameValidateNames()
        {
            string townName = _townName.Value ?? "";
            string playerName = _playerName.Value ?? "";
            _newGameStartButton.interactable = townName.Length > 0 && playerName.Length > 0;
        }

        /// <summary>
        /// Sets the face of the new game player.
        /// </summary>
        /// <param name="name">The face's name.</param>
        public void NewGameSetFace(string name)
        {
            _newGamePlayerFaceImage.sprite = _characterFaces[name].Sprite;
        }

        /// <summary>
        /// Sets the color of the new game player.
        /// </summary>
        /// <param name="name">The color's name.</param>
        public void NewGameSetColor(string name)
        {
            _newGamePlayerFillImage.color = _characterColors[name].Color;
        }
    }
}
