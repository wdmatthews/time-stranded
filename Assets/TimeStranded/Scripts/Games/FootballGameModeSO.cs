using System.Collections.Generic;
using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
    /// Controls data and behavior for the football game mode.
    /// </summary>
    [CreateAssetMenu(fileName = "NewFootballGameMode", menuName = "Time Stranded/Games/Football Game Mode")]
    public class FootballGameModeSO : GameModeSO
    {
        /// <summary>
        /// The number of goals needed to win.
        /// </summary>
        [Tooltip("The number of goals needed to win.")]
        [SerializeField] protected int _goalsToWin = 2;

        /// <summary>
        /// The football to use.
        /// </summary>
        [Tooltip("The football to use.")]
        [SerializeField] protected BallSO _footballData = null;

        /// <summary>
        /// The dodgeball to use.
        /// </summary>
        [Tooltip("The dodgeball to use.")]
        [SerializeField] protected BallSO _dodgeballData = null;

        /// <summary>
        /// The event channel to raise when a goal is scored.
        /// </summary>
        [Tooltip("The event channel to raise when a goal is scored.")]
        [SerializeField] protected CharacterEventChannelSO _onFootballScoreChannel = null;

        /// <summary>
        /// The football.
        /// </summary>
        [System.NonSerialized] protected Ball _football = null;

        /// <summary>
        /// The dodgeballs.
        /// </summary>
        [System.NonSerialized] protected List<Ball> _dodgeballs = new List<Ball>();

        /// <summary>
        /// Spawn the ball and add to team data.
        /// </summary>
        public override void OnStart()
        {

            // Spawn in the football at the center.
            _football = (Ball)_footballData.Pool.Request();
            _football.SetData(_footballData);
            _football.Place(_map.ItemSpawns[0].transform.position);
            int teamCount = _activeTeams.Count;

            for (int i = 0; i < teamCount; i++)
            {
                TeamSO team = _activeTeams[i];
                // Set the football spawn for each team, with the first spawn being at the center.
                team.SpecialSpawns.Add(_map.SpecialSpawns[i]);
                // Set the goal for each team.
                FootballGoal goal = (FootballGoal)_map.Triggers[i];
                goal.Football = _football;
                goal.Team = team;
                goal.OnScore = ScoreGoal;
            }

            // Spawn in the dodgeballs.
            for (int i = _map.ItemSpawns.Count - 2; i >= 0; i--)
            {
                Ball dodgeball = (Ball)_dodgeballData.Pool.Request();
                dodgeball.SetData(_dodgeballData);
                dodgeball.Place(_map.ItemSpawns[i + 1].transform.position);
                _dodgeballs.Add(dodgeball);
            }
        }

        /// <summary>
        /// Recycle the balls.
        /// </summary>
        public override void OnEnd()
        {
            _football.ReturnToPool();
            _football.gameObject.SetActive(false);

            for (int i = _dodgeballs.Count - 1; i >= 0; i--)
            {
                Ball dodgeball = _dodgeballs[i];
                dodgeball.ReturnToPool();
                dodgeball.gameObject.SetActive(false);
            }

            _dodgeballs.Clear();
        }

        /// <summary>
        /// Scores a goal.
        /// </summary>
        /// <param name="character">The character that scored.</param>
        public void ScoreGoal(Character character)
        {
            // Increase the team and character's scores.
            TeamSO team = ActiveTeamsByName[character.Team];
            team.Score++;
            character.Score++;

            // Raise the OnFootballScore event.
            _onFootballScoreChannel?.OnRaised?.Invoke(character);

            // Reset the field or end the match.
            if (team.Score >= _goalsToWin) EndMatch();
            else ResetField(team);
        }

        /// <summary>
        /// Resets the characters and balls.
        /// </summary>
        /// <param name="scoringTeam">The team that scored.</param>
        protected void ResetField(TeamSO scoringTeam)
        {
            // Reset the characters.
            for (int i = _activeTeams.Count - 1; i >= 0; i--)
            {
                TeamSO team = _activeTeams[i];
                team.InitialSpawns = new List<Transform>(team.Spawns);

                for (int j = team.Characters.Count - 1; j >= 0; j--)
                {
                    Character character = team.Characters[j];
                    if (character.IsRespawning) RespawnCharacter(character);
                    else SpawnCharacter(character);
                }
            }

            // Reset the football to the scoring team's spawn.
            _football.Place(scoringTeam.SpecialSpawns[0].position);

            // Reset the dodgeballs.
            for (int i = _dodgeballs.Count - 1; i >= 0; i--)
            {
                _dodgeballs[i].Place(_map.ItemSpawns[i + 1].position);
            }
        }
    }
}
