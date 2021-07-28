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

        /// <summary>
        /// The music clips to play.
        /// </summary>
        [Tooltip("The music clips to play.")]
        [SerializeField] private AudioClipSO[] _musicClips = { };

        /// <summary>
        /// The sound effect clips to play.
        /// </summary>
        [Tooltip("The sound effect clips to play.")]
        [SerializeField] private AudioClipSO[] _sfxClips = { };

        /// <summary>
        /// The audio player used for playing music.
        /// </summary>
        private AudioPlayer _musicPlayer = null;

        private void Start()
        {
            // Update the mixer volumes.
            _audioManager.UpdateMixerVolumes();

            // Play a random music clip.
            PlayMusicClip();

            // Play a random sound effect clip.
            PlaySFXClip();
        }

        /// <summary>
        /// Plays a random music clip.
        /// </summary>
        public void PlayMusicClip()
        {
            AudioClipSO clip = _musicClips[Random.Range(0, _musicClips.Length)];

            // Fade to a new clip if music is already playing.
            if (_musicPlayer)
            {
                _musicPlayer.FadeToClip(clip);
            }
            // Otherwise, start playing music.
            else
            {
                _musicPlayer = _audioManager.Play(clip);
            }
        }

        /// <summary>
        /// Plays a random sound effect clip.
        /// </summary>
        public void PlaySFXClip()
        {
            _audioManager.Play(_sfxClips[Random.Range(0, _sfxClips.Length)]);
        }
    }
}
