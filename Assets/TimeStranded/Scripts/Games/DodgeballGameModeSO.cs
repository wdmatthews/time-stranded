using System.Collections.Generic;
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
        /// The dodgeball to use.
        /// </summary>
        [Tooltip("The dodgeball to use.")]
        [SerializeField] protected BallSO _dodgeballData = null;

        /// <summary>
        /// The dodgeballs.
        /// </summary>
        [System.NonSerialized] protected List<Ball> _dodgeballs = new List<Ball>();

        /// <summary>
        /// Spawn the balls.
        /// </summary>
        public override void OnStart()
        {
            // Spawn the balls.
            for (int i = _map.ItemSpawns.Count - 1; i >= 0; i--)
            {
                Ball dodgeball = (Ball)_dodgeballData.Pool.Request();
                dodgeball.SetData(_dodgeballData);
                dodgeball.Place(_map.ItemSpawns[i].transform.position);
                _dodgeballs.Add(dodgeball);
            }
        }

        /// <summary>
        /// Recycle the balls.
        /// </summary>
        public override void OnEnd()
        {
            for (int i = _dodgeballs.Count - 1; i >= 0; i--)
            {
                Ball dodgeball = _dodgeballs[i];
                dodgeball.ReturnToPool();
                dodgeball.gameObject.SetActive(false);
            }

            _dodgeballs.Clear();
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
