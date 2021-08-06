using UnityEngine;

namespace TimeStranded.Games
{
    /// <summary>
    /// A runtime version of the arena manager.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Arena Manager")]
    [DisallowMultipleComponent]
    public class ArenaManager : MonoBehaviour
    {
        /// <summary>
        /// The arena manager.
        /// </summary>
        [Tooltip("The arena manager.")]
        [SerializeField] private ArenaManagerSO _arenaManager = null;

        private void Update() => _arenaManager.OnUpdate();
    }
}
