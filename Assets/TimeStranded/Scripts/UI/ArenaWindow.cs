using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Toolkits.Events;
using Toolkits.Variables;
using TimeStranded.Characters;
using TimeStranded.Games;

namespace TimeStranded.UI
{
    /// <summary>
    /// Controls the arena game mode selection window.
    /// </summary>
    [AddComponentMenu("Time Stranded/UI/Arena Window")]
    [DisallowMultipleComponent]
    public class ArenaWindow : MonoBehaviour
    {
        /// <summary>
        /// The arena window's rect transform.
        /// </summary>
        [Tooltip("The arena window's rect transform.")]
        [SerializeField] private RectTransform _rectTransform = null;

        /// <summary>
        /// The radio button group for the list of game modes.
        /// </summary>
        [Tooltip("The radio button group for the list of game modes.")]
        [SerializeField] private RadioButtonGroup _gameListGroup = null;

        /// <summary>
        /// The radio button group for the list of maps.
        /// </summary>
        [Tooltip("The radio button group for the list of maps.")]
        [SerializeField] private RadioButtonGroup _mapListGroup = null;

        /// <summary>
        /// The start button.
        /// </summary>
        [Tooltip("The start button.")]
        [SerializeField] private Button _startButton = null;

        /// <summary>
        /// The arena manager.
        /// </summary>
        [Tooltip("The arena manager.")]
        [SerializeField] private ArenaManagerSO _arenaManager = null;

        /// <summary>
        /// The list of player data.
        /// </summary>
        [Tooltip("The list of player data.")]
        [SerializeField] private List<CharacterSO> _players = new List<CharacterSO>();

        /// <summary>
        /// The list of AI data.
        /// </summary>
        [Tooltip("The list of AI data.")]
        [SerializeField] private List<CharacterSO> _ai = new List<CharacterSO>();

        /// <summary>
        /// The duration of the open/close animation.
        /// </summary>
        [Tooltip("The duration of the open/close animation.")]
        [SerializeField] private float _openCloseAnimationDuration = 1;

        /// <summary>
        /// The event channel to raise when opening the arena window.
        /// </summary>
        [Tooltip("The event channel to raise when opening the arena window.")]
        [SerializeField] private EventChannelSO _onArenaWindowOpenChannel = null;

        /// <summary>
        /// The event channel to raise when pausing the game.
        /// </summary>
        [Tooltip("The event channel to raise when pausing the game.")]
        [SerializeField] private EventChannelSO _onPauseChannel = null;

        /// <summary>
        /// The event channel to raise when resuming the game.
        /// </summary>
        [Tooltip("The event channel to raise when resuming the game.")]
        [SerializeField] private EventChannelSO _onResumeChannel = null;

        /// <summary>
        /// The selected game mode's name.
        /// </summary>
        [Tooltip("The selected game mode's name.")]
        [SerializeField] private StringVariableSO _gameModeName = null;

        /// <summary>
        /// The selected arena map's name.
        /// </summary>
        [Tooltip("The selected arena map's name.")]
        [SerializeField] private StringVariableSO _arenaMapName = null;

        /// <summary>
        /// The storyline.
        /// </summary>
        [Tooltip("The storyline.")]
        [SerializeField] private StringVariableSO _storyline = null;

        /// <summary>
        /// A list of future game modes.
        /// </summary>
        [Tooltip("A list of future game modes.")]
        [SerializeField] private List<GameModeSO> _futureGameModes = new List<GameModeSO>();

        /// <summary>
        /// A list of past game modes.
        /// </summary>
        [Tooltip("A list of past game modes.")]
        [SerializeField] private List<GameModeSO> _pastGameModes = new List<GameModeSO>();

        /// <summary>
        /// A list of game modes.
        /// </summary>
        private List<GameModeSO> _gameModes = null;

        /// <summary>
        /// A dictionary of game modes organized by name.
        /// </summary>
        private Dictionary<string, GameModeSO> _gameModesByName = new Dictionary<string, GameModeSO>();

        /// <summary>
        /// A list of game mode names.
        /// </summary>
        private List<string> _gameModeNames = new List<string>();

        /// <summary>
        /// A list of arena map names.
        /// </summary>
        private List<string> _arenaMapNames = new List<string>();

        /// <summary>
        /// The name of the previously selected game mode.
        /// </summary>
        private string _previousGameName = "";

        /// <summary>
        /// The name of the previously selected arena map.
        /// </summary>
        private string _previousMapName = "";

        private void Awake()
        {
            if (_onArenaWindowOpenChannel) _onArenaWindowOpenChannel.OnRaised += Open;
            gameObject.SetActive(false);

            // Get the game mode names.
            _gameModes = _storyline.Value == "Future" ? _futureGameModes : _pastGameModes;
            int gameModeCount = _gameModes.Count;

            for (int i = 0; i < gameModeCount; i++)
            {
                GameModeSO gameMode = _gameModes[i];
                _gameModeNames.Add(gameMode.name);
                _gameModesByName.Add(gameMode.name, gameMode);
            }
        }

        private void OnDestroy()
        {
            if (_onArenaWindowOpenChannel) _onArenaWindowOpenChannel.OnRaised -= Open;
        }

        /// <summary>
        /// Opens the arena window.
        /// </summary>
        private void Open()
        {
            // Open the window.
            gameObject.SetActive(true);
            _rectTransform.DOAnchorPosY(0, _openCloseAnimationDuration)
                .From(new Vector2(0, -_rectTransform.rect.height))
                .onComplete += _onPauseChannel.Raise;
            // Initialize the game and arena list groups.
            _gameListGroup.Initialize(_gameModeNames);
            _arenaMapNames.Clear();
            GameModeSO gameMode = _gameModesByName[_gameModeNames[0]];
            int mapCount = gameMode.Maps.Length;

            for (int i = 0; i < mapCount; i++)
            {
                _arenaMapNames.Add(gameMode.Maps[i].name);
            }

            _mapListGroup.Initialize(_arenaMapNames);
        }

        /// <summary>
        /// Closes the arena window.
        /// </summary>
        public void Close()
        {
            _rectTransform.DOAnchorPosY(-_rectTransform.rect.height, _openCloseAnimationDuration)
                .From(new Vector2())
                .onComplete += Hide;
        }

        /// <summary>
        /// Hides the arena window.
        /// </summary>
        private void Hide()
        {
            _onResumeChannel?.Raise();
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Validates the game mode and arena map name for a match.
        /// </summary>
        public void ValidateNames()
        {
            // A StringVariableSO's default value is null.
            string gameModeName = _gameModeName.Value ?? "";
            string arenaMapName = _arenaMapName.Value ?? "";
            _startButton.interactable = gameModeName.Length > 0 && arenaMapName.Length > 0;

            // Update the arena map list as needed.
            if (gameModeName != _previousGameName)
            {
                _arenaMapNames.Clear();

                if (gameModeName.Length > 0)
                {
                    GameModeSO gameMode = _gameModesByName[gameModeName];
                    int mapCount = gameMode.Maps.Length;

                    for (int i = 0; i < mapCount; i++)
                    {
                        _arenaMapNames.Add(gameMode.Maps[i].name);
                    }
                }

                _mapListGroup.Initialize(_arenaMapNames);
            }

            _previousGameName = gameModeName;
            _previousMapName = arenaMapName;
        }

        /// <summary>
        /// Starts a match.
        /// </summary>
        public void StartMatch()
        {
            GameModeSO gameMode = _gameModesByName[_gameModeName.Value];
            ArenaMap map = gameMode.GetMap(_arenaMapName.Value);
            // TODO Generate list of AI characters.
            _arenaManager.Load(gameMode, map, _players, _ai, gameMode.Teams.Count > 0 ? gameMode.Teams : null);
        }
    }
}
