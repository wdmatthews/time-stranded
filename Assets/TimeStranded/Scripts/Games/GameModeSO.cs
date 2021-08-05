using System.Collections.Generic;
using UnityEngine;
using Toolkits.Events;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
    /// Stores data about and provides functionality for a game mode.
    /// </summary>
    [CreateAssetMenu(fileName = "NewGameMode", menuName = "Time Stranded/Games/Game Mode")]
    public class GameModeSO : ScriptableObject
    {
        /// <summary>
        /// The maximum number of respawns per character.
        /// </summary>
        [Tooltip("The maximum number of respawns per character.")]
        [SerializeField] protected int _maxRespawns = 1;

        /// <summary>
        /// The number of seconds it takes to respawn.
        /// </summary>
        [Tooltip("The number of seconds it takes to respawn.")]
        [SerializeField] protected float _respawnTime = 1;

        /// <summary>
        /// The list of all teams involved in a match.
        /// </summary>
        [Tooltip("The list of all teams involved in a match.")]
        [SerializeField] protected TeamListReferenceSO _activeTeams = null;

        /// <summary>
        /// The list of all characters involved in a match.
        /// </summary>
        [Tooltip("The list of all characters involved in a match.")]
        [SerializeField] protected CharacterListReferenceSO _activeCharacters = null;

        /// <summary>
        /// The list of all alive characters involved in a match.
        /// </summary>
        [Tooltip("The list of all alive characters involved in a match.")]
        [SerializeField] protected CharacterListReferenceSO _aliveCharacters = null;

        /// <summary>
        /// The list of player characters involved in a match.
        /// </summary>
        [Tooltip("The list of player characters involved in a match.")]
        [SerializeField] protected CharacterListReferenceSO _activePlayers = null;

        /// <summary>
        /// The list of AI characters involved in a match.
        /// </summary>
        [Tooltip("The list of AI characters involved in a match.")]
        [SerializeField] protected CharacterListReferenceSO _activeAI = null;

        /// <summary>
        /// The event channel raised when a match is started.
        /// </summary>
        [Tooltip("The event channel raised when a match is started.")]
        [SerializeField] protected EventChannelSO _onMatchStart = null;

        /// <summary>
        /// The event channel raised when a match is ended.
        /// </summary>
        [Tooltip("The event channel raised when a match is ended.")]
        [SerializeField] protected EventChannelSO _onMatchEnd = null;

        /// <summary>
        /// The event channel to raise when a character is healed.
        /// </summary>
        [Tooltip("The event channel to raise when a character is healed.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterHeal = null;

        /// <summary>
        /// The event channel to raise when a character is damaged.
        /// </summary>
        [Tooltip("The event channel to raise when a character is damaged.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterDamage = null;

        /// <summary>
        /// The event channel to raise when a character dies.
        /// </summary>
        [Tooltip("The event channel to raise when a character dies.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterDeath = null;

        /// <summary>
        /// A dictionary of all teams in a match organized by name.
        /// </summary>
        [System.NonSerialized] public Dictionary<string, TeamSO> ActiveTeamsByName = new Dictionary<string, TeamSO>();

        /// <summary>
        /// Whether or not this game mode was started.
        /// </summary>
        [System.NonSerialized] public bool WasStarted = false;

        /// <summary>
        /// Clears all match data.
        /// </summary>
        protected virtual void ClearMatch()
        {
            _activeTeams.Clear();
            ActiveTeamsByName.Clear();
            _activeCharacters.Clear();
            _activePlayers.Clear();
            _activeAI.Clear();
        }

        /// <summary>
        /// Randomly places characters on teams.
        /// </summary>
        /// <param name="characters">The characters to place.</param>
        /// <param name="teams">The list of teams in the match.</param>
        /// <param name="teamIndex">The current team index.</param>
        /// <returns>The next team index.</returns>
        protected virtual int RandomlyPlaceOnTeams(List<Character> characters, List<TeamSO> teams, int teamIndex)
        {
            int index = teamIndex;
            int teamCount = teams.Count;

            for (int i = characters.Count - 1; i >= 0; i--)
            {
                // Assign a team to the character, then go to the next team.
                characters[i].Team = teams[teamIndex].name;
                teamIndex++;
                if (teamIndex >= teamCount) teamIndex = 0;
            }

            return index;
        }

        /// <summary>
        /// Places characters on teams, either randomly, or based on each Character's Team field.
        /// </summary>
        /// <param name="players">The list of players joining the match.</param>
        /// <param name="ai">The list of AI joining the match.</param>
        /// <param name="teams">The list of teams in the match. Null on solo game modes.</param>
        /// <param name="randomlyChooseTeams">Whether or not to randomly pick teams.</param>
        protected virtual void ChooseTeams(List<Character> players, List<Character> ai,
            List<TeamSO> teams = null, bool randomlyChooseTeams = true)
        {
            int playerCount = players.Count;
            int aiCount = ai.Count;

            // Reset character teams if in solo.
            if (teams == null)
            {
                for (int i = 0; i < playerCount; i++)
                {
                    players[i].Team = "";
                }

                for (int i = 0; i < aiCount; i++)
                {
                    ai[i].Team = "";
                }

                return;
            }

            // Only place characters on teams if in a team game mode.
            if (randomlyChooseTeams)
            {
                // Shuffle the characters and teams.
                List<Character> shuffledPlayers = new List<Character>(players);
                List<Character> shuffledAI = new List<Character>(ai);
                List<TeamSO> shuffledTeams = new List<TeamSO>(teams);
                shuffledPlayers.Shuffle();
                shuffledAI.Shuffle();
                shuffledTeams.Shuffle();

                // Places the characters on the teams.
                int teamIndex = Random.Range(0, teams.Count);
                teamIndex = RandomlyPlaceOnTeams(shuffledPlayers, shuffledTeams, teamIndex);
                RandomlyPlaceOnTeams(shuffledAI, shuffledTeams, teamIndex);
            }

            // Add the players to the team data.
            for (int i = 0; i < playerCount; i++)
            {
                Character character = players[i];
                ActiveTeamsByName[character.Team].Characters.Add(character);
            }

            for (int i = 0; i < aiCount; i++)
            {
                Character character = ai[i];
                ActiveTeamsByName[character.Team].Characters.Add(character);
            }
        }

        /// <summary>
        /// Starts a match with the given players, AI, and teams (if any).
        /// No teams means solo, but if there are teams and <paramref name="randomlyChooseTeams"/> is true,
        /// players and AI will automatically be placed into teams.
        /// If <paramref name="randomlyChooseTeams"/> is false, characters will need
        /// their Team field set to be placed on the right team.
        /// </summary>
        /// <param name="players">The list of players joining the match.</param>
        /// <param name="ai">The list of AI joining the match.</param>
        /// <param name="teams">The list of teams in the match. Null on solo game modes.</param>
        /// <param name="randomlyChooseTeams">Whether or not to randomly pick teams.</param>
        public void StartMatch(List<Character> players, List<Character> ai,
            List<TeamSO> teams = null, bool randomlyChooseTeams = true)
        {
            // Subscribe to character events.
            if (_onCharacterHeal) _onCharacterHeal.OnRaised += OnCharacterHeal;
            if (_onCharacterDamage) _onCharacterDamage.OnRaised += OnCharacterDamage;
            if (_onCharacterDeath) _onCharacterDeath.OnRaised += OnCharacterDeath;

            // Clear data.
            ClearMatch();
            // Update lists.
            if (teams != null) _activeTeams.AddRange(teams);
            _activeCharacters.AddRange(players);
            _activeCharacters.AddRange(ai);
            _activePlayers.AddRange(players);
            _activeAI.AddRange(ai);

            // Get teams by name.
            for (int i = teams != null ? teams.Count - 1 : -1; i >= 0; i--)
            {
                TeamSO team = teams[i];
                ActiveTeamsByName.Add(team.name, team);
                team.Characters.Clear();
            }

            // Reset character data as needed.
            for (int i = _activeCharacters.Count - 1; i >= 0; i--)
            {
                Character character = _activeCharacters[i];
                character.RespawnsLeft = _maxRespawns;
                character.TimeUntilRespawn = _respawnTime;
                character.IsRespawning = false;
                character.Respawn = RespawnCharacter;
            }

            // Choose teams and start the match.
            ChooseTeams(players, ai, teams, randomlyChooseTeams);
            OnStart();
            WasStarted = true;
            _onMatchStart?.Raise();
        }

        /// <summary>
        /// Called when the match is started.
        /// </summary>
        public virtual void OnStart() { }

        /// <summary>
        /// Called every update tick.
        /// </summary>
        public virtual void OnUpdate()
        {
            for (int i = _activeCharacters.Count - 1; i >= 0; i--)
            {
                _activeCharacters[i].OnUpdateInMatch();
            }
        }

        /// <summary>
        /// Called when the match is ended.
        /// </summary>
        public virtual void OnEnd() { }

        /// <summary>
        /// Ends the match.
        /// </summary>
        public void EndMatch()
        {
            OnEnd();
            WasStarted = false;

            // Unsubscribe from character events, as they are not needed after a match ends.
            if (_onCharacterHeal) _onCharacterHeal.OnRaised -= OnCharacterHeal;
            if (_onCharacterDamage) _onCharacterDamage.OnRaised -= OnCharacterDamage;
            if (_onCharacterDeath) _onCharacterDeath.OnRaised -= OnCharacterDeath;

            _onMatchEnd?.Raise();
        }

        /// <summary>
        /// Called when a character is healed.
        /// </summary>
        /// <param name="character">The character that was healed.</param>
        protected virtual void OnCharacterHeal(Character character) { }

        /// <summary>
        /// Called when a character is damaged.
        /// </summary>
        /// <param name="character">The character that was damaged.</param>
        protected virtual void OnCharacterDamage(Character character) { }

        /// <summary>
        /// Called when a character dies.
        /// </summary>
        /// <param name="character">The character that died.</param>
        protected virtual void OnCharacterDeath(Character character)
        {
            // Remove the character from the alive list.
            _aliveCharacters.Remove(character);

            // Attempt to start respawning the character.
            if (character.RespawnsLeft > 0)
            {
                character.RespawnsLeft--;
                character.TimeUntilRespawn = _respawnTime;
                character.IsRespawning = true;
            }
        }

        /// <summary>
        /// Respawns the given character.
        /// </summary>
        /// <param name="character"></param>
        protected virtual void RespawnCharacter(Character character)
        {
            // Add the character to the alive list.
            _aliveCharacters.Add(character);
            character.IsRespawning = false;
        }
    }
}
