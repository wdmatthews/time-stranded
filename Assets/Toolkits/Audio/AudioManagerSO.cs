using UnityEngine;
using UnityEngine.Audio;

namespace Toolkits.Audio
{
    /// <summary>
    /// Manages playing audio clips.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioManager", menuName = "Toolkits/Audio/Audio Manager")]
    public class AudioManagerSO : ScriptableObject
    {
        /// <summary>
        /// The audio mixer used by this manager.
        /// </summary>
        public AudioMixer Mixer = null;
    }
}
