using UnityEngine;

namespace TimeStranded.Management
{
    /// <summary>
    /// Provides the Awake and Start methods to a game manager.
    /// </summary>
    [AddComponentMenu("Time Stranded/Management/Game Manager")]
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The game manager.
        /// </summary>
        [Tooltip("The game manager.")]
        [SerializeField] private GameManagerSO _gameManager = null;

        private void Awake() => _gameManager.OnAwake();

        private void Start() => _gameManager.OnStart();
    }
}
