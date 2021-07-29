using UnityEngine;

namespace Toolkits.Loading
{
    /// <summary>
    /// A <see cref="MonoBehaviour"/> used to provide Unity methods to a <see cref="LoadManagerSO"/>.
    /// </summary>
    [AddComponentMenu("Toolkits/Loading/Load Manager")]
    public class LoadManager : MonoBehaviour
    {
        /// <summary>
        /// The load manager to give methods to.
        /// </summary>
        [Tooltip("The load manager to give methods to.")]
        [SerializeField] private LoadManagerSO _loadManager = null;

        private void Awake()
        {
            _loadManager.StartCoroutine = StartCoroutine;
        }
    }
}
