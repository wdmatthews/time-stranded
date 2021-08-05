using System.Collections.Generic;
using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
    /// Stores team data.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTeam", menuName = "Time Stranded/Games/Team")]
    public class TeamSO : ScriptableObject
    {
        /// <summary>
        /// The team's characters.
        /// Only used during a match.
        /// </summary>
        [System.NonSerialized] public List<Character> Characters = new List<Character>();

        /// <summary>
        /// The teams's score.
        /// Only used during a match.
        /// </summary>
        [System.NonSerialized] public int Score = 0;
    }
}
