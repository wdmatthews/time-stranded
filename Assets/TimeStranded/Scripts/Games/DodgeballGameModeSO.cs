using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
    /// Controls data and behavior for the dodgeball game mode.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDodgeballGameMode", menuName = "Time Stranded/Games/Dodgeball Game Mode")]
    public class DodgeballGameModeSO : GameModeSO
    {
        /// <summary>
        /// The ball to use.
        /// </summary>
        [Tooltip("The ball to use.")]
        [SerializeField] protected BallSO _ballData = null;

        /// <summary>
        /// Spawn the balls.
        /// </summary>
        public override void OnStart()
        {
            base.OnStart();

            for (int i = _map.BallSpawns.Count - 1; i >= 0; i--)
            {
                Ball ball = (Ball)_ballData.Pool.Request();
                ball.SetData(_ballData);
                ball.transform.position = _map.BallSpawns[i].transform.position;
            }
        }

        /// <summary>
        /// Increases the score when characters die.
        /// </summary>
        /// <param name="character">The character that died.</param>
        protected override void OnCharacterDeath(Character character)
        {
            base.OnCharacterDeath(character);
            // Do nothing if the character died to themselves.
            if (character.DeathItem == null || character.DeathItem.Character == character) return;
            // Increase the character's score, and its team score if it has one.
            character.Score++;
            if (character.Team.Length > 0) ActiveTeamsByName[character.Team].Score++;

            int teamCount = _activeTeams.Count;
            int aliveCount = _aliveCharacters.Count;
            int respawningCount = _respawningCharacters.Count;

            // Solo win because the only alive character is the winner and none are respawning.
            if (teamCount == 0 && aliveCount == 1 && respawningCount == 0)
            {
                EndMatch();
            }
            // Team win because the only alive or respawning characters are on the same team.
            else if (teamCount > 0)
            {
                string teamName = "";

                // Check if all alive characters are on the same team.
                for (int i = 0; i < aliveCount; i++)
                {
                    Character aliveCharacter = _aliveCharacters[i];
                    if (teamName.Length == 0) teamName = aliveCharacter.Team;
                    else if (teamName != aliveCharacter.Team) return;
                }

                // Check if all respawning characters are on the same team as the alive characters.
                for (int i = 0; i < respawningCount; i++)
                {
                    Character respawningCharacter = _respawningCharacters[i];
                    if (teamName.Length == 0) teamName = respawningCharacter.Team;
                    else if (teamName != respawningCharacter.Team) return;
                }

                EndMatch();
            }
        }
    }
}
