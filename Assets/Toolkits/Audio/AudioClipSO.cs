using UnityEngine;

namespace Toolkits.Audio
{
    /// <summary>
    /// Stores information about an audio clip.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioClip", menuName = "Toolkits/Audio/Audio Clip")]
    public class AudioClipSO : ScriptableObject
    {
        /// <summary>
        /// The audio clip.
        /// </summary>
        [Tooltip("The audio clip.")]
        public AudioClip Clip = null;

        /// <summary>
        /// The audio group this clip belongs to.
        /// </summary>
        [Tooltip("The audio group this clip belongs to.")]
        public AudioGroupSO Group = null;

        /// <summary>
        /// Whether or not the audio clip should loop.
        /// </summary>
        [Tooltip("Whether or not the audio clip should loop.")]
        public bool ShouldLoop = false;

        /// <summary>
        /// The minimum volume when randomly picking a volume to play this clip.
        /// </summary>
        [Tooltip("The minimum volume when randomly picking a volume to play this clip.")]
        [Range(0, 1)] public float MinVolume = 1;

        /// <summary>
        /// The maximum volume when randomly picking a volume to play this clip.
        /// </summary>
        [Tooltip("The maximum volume when randomly picking a volume to play this clip.")]
        [Range(0, 1)] public float MaxVolume = 1;

        /// <summary>
        /// The minimum pitch when randomly picking a volume to play this clip.
        /// </summary>
        [Tooltip("The minimum pitch when randomly picking a volume to play this clip.")]
        [Range(-3, 3)] public float MinPitch = 1;

        /// <summary>
        /// The maximum pitch when randomly picking a volume to play this clip.
        /// </summary>
        [Tooltip("The maximum pitch when randomly picking a volume to play this clip.")]
        [Range(-3, 3)] public float MaxPitch = 1;
    }
}
