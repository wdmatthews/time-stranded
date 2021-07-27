using UnityEngine;

namespace Toolkits.Enums.Test
{
    /// <summary>
    /// Used to test the <see cref="ScriptableObject"/> enum pattern.
    /// </summary>
    [AddComponentMenu("Toolkits/Enums/Test/Test")]
    [DisallowMultipleComponent]
    public class EnumsTest : MonoBehaviour
    {
        /// <summary>
        /// The test team for player 1.
        /// </summary>
        [Tooltip("The test team for player 1.")]
        public TestEnumSO Player1Team = null;

        /// <summary>
        /// The test team for player 2.
        /// </summary>
        [Tooltip("The test team for player 2.")]
        public TestEnumSO Player2Team = null;

        private void Start()
        {
            // Log if the players are on the same team.
            if (Player1Team == Player2Team)
            {
                Debug.Log("Player 1 is on the same team as player 2.");
            }
            else
            {
                Debug.Log("Player 1 is on a different team than player 2.");
            }

            // Log player 1's team.
            Player1Team.LogNameAndColor();

            // Log player 2's team.
            Player2Team.LogNameAndColor();
        }
    }
}
