using System.Collections.Generic;
using UnityEngine;
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
        /// The game mode to test.
        /// </summary>
        [Tooltip("The game mode to test.")]
        [SerializeField] private GameModeSO _gameMode = null;

        /// <summary>
        /// The list of player characters.
        /// </summary>
        [Tooltip("The list of player characters.")]
        [SerializeField] private List<Character> _players = new List<Character>();

        /// <summary>
        /// The list of AI characters.
        /// </summary>
        [Tooltip("The list of AI characters.")]
        [SerializeField] private List<Character> _ai = new List<Character>();

        /// <summary>
        /// The list of teams.
        /// </summary>
        [Tooltip("The list of teams.")]
        [SerializeField] private List<TeamSO> _teams = new List<TeamSO>();

        private void Start()
        {
            // Start and end match with randomly picked teams.
            _gameMode.StartMatch(_players, _ai, _teams);
        }
    }
}
