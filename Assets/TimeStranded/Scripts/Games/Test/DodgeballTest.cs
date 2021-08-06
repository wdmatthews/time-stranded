using System.Collections.Generic;
using UnityEngine;
using Toolkits.Events;
using TimeStranded.Characters;

namespace TimeStranded.Games.Test
{
    /// <summary>
    /// Used to test the dodgeball game mode.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Test/Dodgeball Test")]
    [DisallowMultipleComponent]
    public class DodgeballTest : MonoBehaviour
    {
        /// <summary>
        /// The arena manager.
        /// </summary>
        [Tooltip("The arena manager.")]
        [SerializeField] private ArenaManagerSO _arenaManager = null;

        /// <summary>
        /// The game mode to test.
        /// </summary>
        [Tooltip("The game mode to test.")]
        [SerializeField] private GameModeSO _gameMode = null;

        /// <summary>
        /// The event channel raised when a match is ended.
        /// </summary>
        [Tooltip("The event channel raised when a match is ended.")]
        [SerializeField] protected EventChannelSO _onMatchEnd = null;

        /// <summary>
        /// The map to test.
        /// </summary>
        [Tooltip("The map to test.")]
        [SerializeField] private ArenaMap _map = null;

        /// <summary>
        /// The list of player characters.
        /// </summary>
        [Tooltip("The list of player characters.")]
        [SerializeField] private List<CharacterSO> _players = new List<CharacterSO>();

        /// <summary>
        /// The list of AI characters.
        /// </summary>
        [Tooltip("The list of AI characters.")]
        [SerializeField] private List<CharacterSO> _ai = new List<CharacterSO>();

        /// <summary>
        /// The list of teams.
        /// </summary>
        [Tooltip("The list of teams.")]
        [SerializeField] private List<TeamSO> _teams = new List<TeamSO>();

        private void Start()
        {
            // Subscribe to the load event first.
            _arenaManager.OnArenaLoadChannel.OnRaised += StartMatch;
            _onMatchEnd.OnRaised += UnloadArena;
            // Then load the arena.
            _arenaManager.Load(_gameMode, _map, _players, _ai, _teams);
        }

        /// <summary>
        /// Starts the match.
        /// </summary>
        private void StartMatch() => _arenaManager.StartMatch();

        /// <summary>
        /// Unloads the arena after the match.
        /// </summary>
        private void UnloadArena() => _arenaManager.Unload();
    }
}
