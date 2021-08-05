using System.Collections.Generic;
using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games.Test
{
    /// <summary>
    /// Used to test game modes.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Test/Game Mode Test")]
    [DisallowMultipleComponent]
    public class GameModeTest : MonoBehaviour
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
            Debug.Log("Random teams match started.");
            //_gameMode.EndMatch();
            //Debug.Log("Random teams match ended.");

            // Start and end match with pre-chosen teams.
            //for (int i = _players.Count - 1; i >= 0; i--)
            //{
            //    _players[i].Team = _teams[i].name;
            //}

            //for (int i = _ai.Count - 1; i >= 0; i--)
            //{
            //    _ai[i].Team = _teams[i].name;
            //}

            //_gameMode.StartMatch(_players, _ai, _teams, false);
            //Debug.Log("Selected teams match started.");
            //_gameMode.EndMatch();
            //Debug.Log("Selected teams match ended.");

            // Start and end match with no teams.
            //_gameMode.StartMatch(_players, _ai);
            //Debug.Log("Solo match started.");
            //_gameMode.EndMatch();
            //Debug.Log("Solo match ended.");
        }
    }
}
