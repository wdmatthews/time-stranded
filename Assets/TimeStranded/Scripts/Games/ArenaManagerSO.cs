using System.Collections.Generic;
using UnityEngine;
using TimeStranded.Characters;
using Toolkits.Events;
using TimeStranded.Locations;

namespace TimeStranded.Games
{
    /// <summary>
    /// Manages the arena and loading of game modes.
    /// </summary>
    [CreateAssetMenu(fileName = "NewArenaManager", menuName = "Time Stranded/Games/Arena Manager")]
    public class ArenaManagerSO : ScriptableObject
    {
        /// <summary>
        /// The location manager used to load and unload the arena.
        /// </summary>
        [Tooltip("The location manager used to load and unload the arena.")]
        [SerializeField] private LocationManagerSO _locationManager = null;

        /// <summary>
        /// The arena location.
        /// </summary>
        [Tooltip("The arena location.")]
        [SerializeField] private LocationSO _location = null;

        /// <summary>
        /// The event channel to raise when the arena finishes loading.
        /// </summary>
        [Tooltip("The event channel to raise when the arena finishes loading.")]
        public EventChannelSO OnArenaLoadChannel = null;

        /// <summary>
        /// The currently loaded game mode.
        /// </summary>
        [System.NonSerialized] private GameModeSO _gameMode = null;

        /// <summary>
        /// The arena map to load.
        /// </summary>
        [System.NonSerialized] private ArenaMap _mapToLoad = null;

        /// <summary>
        /// The currently loaded arena map.
        /// </summary>
        [System.NonSerialized] private ArenaMap _map = null;

        /// <summary>
        /// The list of player to load.
        /// </summary>
        [System.NonSerialized] private List<CharacterSO> _playersToLoad = null;

        /// <summary>
        /// The list of AI to load.
        /// </summary>
        [System.NonSerialized] private List<CharacterSO> _aiToLoad = null;

        /// <summary>
        /// The list of player instances.
        /// </summary>
        [System.NonSerialized] private List<Character> _players = new List<Character>();

        /// <summary>
        /// The list of AI instances.
        /// </summary>
        [System.NonSerialized] private List<Character> _ai = new List<Character>();

        /// <summary>
        /// The list of teams.
        /// </summary>
        [System.NonSerialized] private List<TeamSO> _teams = null;

        /// <summary>
        /// Whether or not the teams are randomly chosen.
        /// </summary>
        [System.NonSerialized] private bool _randomlyChooseTeams = true;

        /// <summary>
        /// Called every Update.
        /// </summary>
        public void OnUpdate()
        {
            if (_gameMode && _gameMode.WasStarted) _gameMode.OnUpdate();
        }

        /// <summary>
        /// Loads in the arena location with the given map and game mode.
        /// </summary>
        /// <param name="gameMode">The game mode to load.</param>
        /// <param name="map">The map to load.</param>
        /// <param name="players">The list of players joining the match.</param>
        /// <param name="ai">The list of AI joining the match.</param>
        /// <param name="teams">The list of teams in the match. Null on solo game modes.</param>
        /// <param name="randomlyChooseTeams">Whether or not to randomly pick teams.</param>
        public void Load(GameModeSO gameMode, ArenaMap map,
            List<CharacterSO> players, List<CharacterSO> ai,
            List<TeamSO> teams = null, bool randomlyChooseTeams = true)
        {
            // Initialize some data.
            _gameMode = gameMode;
            _mapToLoad = map;
            _playersToLoad = players;
            _aiToLoad = ai;
            _teams = teams;
            _randomlyChooseTeams = randomlyChooseTeams;

            // Start loading the arena location.
            _locationManager.OnLocationSceneLoadFinishChannel.OnRaised += OnLoadFinish;
            _locationManager.LoadLocation(_location);
        }

        /// <summary>
        /// Loads the arena.
        /// </summary>
        /// <param name="location"></param>
        /// <param name="progress"></param>
        private void OnLoadFinish(LocationSO location, float progress)
        {
            _map = Instantiate(_mapToLoad);

            // Instantiate the players and AI.
            for (int i = _playersToLoad.Count - 1; i >= 0; i--)
            {
                CharacterSO characterData = _playersToLoad[i];
                Character character = Instantiate(characterData.ArenaPrefab);
                character.Initialize(characterData);
                _players.Add(character);
            }

            for (int i = _aiToLoad.Count - 1; i >= 0; i--)
            {
                CharacterSO characterData = _aiToLoad[i];
                Character character = Instantiate(characterData.ArenaPrefab);
                character.Initialize(characterData);
                _ai.Add(character);
            }

            OnArenaLoadChannel.Raise();
        }

        /// <summary>
        /// Unloads the arena.
        /// </summary>
        public void Unload()
        {
            _gameMode = null;
            _mapToLoad = null;
            _map = null;
            _playersToLoad = null;
            _aiToLoad = null;
            _players.Clear();
            _ai.Clear();
            _teams = null;
            _locationManager.OnLocationSceneLoadFinishChannel.OnRaised -= OnLoadFinish;
        }

        /// <summary>
        /// Starts the match.
        /// </summary>
        public void StartMatch() => _gameMode.StartMatch(_players, _ai, _map, _teams, _randomlyChooseTeams);
    }
}
