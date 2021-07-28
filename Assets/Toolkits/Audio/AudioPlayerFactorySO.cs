using UnityEngine;
using Toolkits.ObjectPooling;

namespace Toolkits.Audio
{
    /// <summary>
    /// Used to create <see cref="AudioPlayer"/>s.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioPlayerFactory", menuName = "Toolkits/Audio/Audio Player Factory")]
    public class AudioPlayerFactorySO : FactorySO<AudioPlayer>
    {
        /// <summary>
        /// The prefab to instantiate when creating an instance.
        /// </summary>
        [Tooltip("The prefab to instantiate when creating an instance.")]
        [SerializeField] private AudioPlayer _prefab = null;

        /// <summary>
        /// Creates and returns a new instance of an audio player.
        /// </summary>
        /// <returns>The new instance of the audio player.</returns>
        public override AudioPlayer Create()
        {
            AudioPlayer audioPlayer = Instantiate(_prefab);
            audioPlayer.gameObject.SetActive(false);
            return audioPlayer;
        }
    }
}
