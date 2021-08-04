using UnityEngine;

namespace TimeStranded.Games
{
    /// <summary>
    /// Provides Unity methods to a <see cref="GameModeSO"/>.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Game Mode")]
    [DisallowMultipleComponent]
    public class GameMode : MonoBehaviour
    {
        /// <summary>
        /// The game mode data.
        /// </summary>
        [Tooltip("The game mode data.")]
        [SerializeField] private GameModeSO _gameMode = null;

        private void Update() => _gameMode.OnUpdate();
    }
}
