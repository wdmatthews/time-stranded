using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
    /// A football goal.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Football Goal")]
    [DisallowMultipleComponent]
    public class FootballGoal : Trigger
    {
        /// <summary>
        /// The football.
        /// </summary>
        [System.NonSerialized] public Ball Football = null;

        /// <summary>
        /// The team that this goal belongs to.
        /// </summary>
        [System.NonSerialized] public TeamSO Team = null;

        /// <summary>
        /// The method to call when scoring a goal.
        /// </summary>
        [System.NonSerialized] public System.Action<Character> OnScore = null;

        /// <summary>
        /// Scores the goal.
        /// </summary>
        /// <param name="collider"></param>
        protected override void OnEnter(Collider2D collider)
        {
            Ball ball = collider.GetComponent<Ball>();
            if (ball != Football) return;
            Character scoringCharacter = (Character)ball.Character;
            if (scoringCharacter.Team == Team.name) ball.Bounce();
            else OnScore(scoringCharacter);
        }
    }
}
