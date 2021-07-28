using UnityEngine;
using UnityEngine.Audio;

namespace Toolkits.Audio
{
    /// <summary>
    /// Stores information about an audio group.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMusicClip", menuName = "Toolkits/Audio/Audio Group")]
    public class AudioGroupSO : ScriptableObject
    {
        /// <summary>
        /// The mixer group used for this audio group.
        /// </summary>
        [Tooltip("The mixer group used for this audio group.")]
        public AudioMixerGroup MixerGroup = null;

        /// <summary>
        /// The volume to use when playing a clip in this group.
        /// </summary>
        [Tooltip("The volume to use when playing a clip in this group.")]
        [Range(0, 1)] public float Volume = 1;

        /// <summary>
        /// The name of this group's exposed volume parameter.
        /// </summary>
        [Tooltip("The name of this group's exposed volume parameter.")]
        public string VolumeParameterName = "";
    }
}
