using UnityEngine;

namespace Toolkits.Audio.Test
{
    /// <summary>
    /// Used to test the audio manager.
    /// </summary>
    [AddComponentMenu("Toolkits/Audio/Test/Test")]
    [DisallowMultipleComponent]
    public class AudioTest : MonoBehaviour
    {
        /// <summary>
        /// The audio manager to test.
        /// </summary>
        [Tooltip("The audio manager to test.")]
        [SerializeField] private AudioManagerSO _audioManager = null;

        private void Start()
        {
            
        }
    }
}
