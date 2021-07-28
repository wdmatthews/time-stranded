using UnityEngine;
using UnityEngine.Audio;
using Toolkits.EventSystem;

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
        [Tooltip("The audio mixer used by this manager.")]
        public AudioMixer Mixer = null;

        /// <summary>
        /// The audio groups used by this manager.
        /// </summary>
        [Tooltip("The audio groups used by this manager.")]
        public AudioGroupSO[] AudioGroups = { };

        /// <summary>
        /// The pool used for audio players.
        /// </summary>
        [Tooltip("The pool used for audio players.")]
        [SerializeField] private AudioPlayerPoolSO _audioPlayerPool = null;

        /// <summary>
        /// The event channel to use when finished playing an audio clip.
        /// </summary>
        [Tooltip("The event channel to use when finished playing an audio clip.")]
        [SerializeField] private AudioPlayerEventChannelSO _onFinishedPlayingChannel = null;

        /// <summary>
        /// The event channel to use when pausing audio
        /// </summary>
        [Tooltip("The event channel to use when pausing audio.")]
        [SerializeField] private EventChannelSO _onPauseChannel = null;

        /// <summary>
        /// The event channel to use when resuming audio
        /// </summary>
        [Tooltip("The event channel to use when resuming audio.")]
        [SerializeField] private EventChannelSO _onResumeChannel = null;

        /// <summary>
        /// Used to determine if a subscription to OnFinishedPlaying is needed before playing an audio clip.
        /// </summary>
        [System.NonSerialized] private bool _subscribedToOnFinishedPlaying = false;

        /// <summary>
        /// Updates the volumes in the audio mixer.
        /// </summary>
        public void UpdateMixerVolumes()
        {
            for (int i = AudioGroups.Length - 1; i >= 0; i--)
            {
                AudioGroupSO group = AudioGroups[i];
                Mixer.SetFloat(group.VolumeParameterName, NormalizedToMixerVolume(group.Volume));
            }
        }

        /// <summary>
        /// Returns a volume in decibels, ranging from -80 to 0.
        /// </summary>
        /// <param name="normalizedVolume">A normalized volume, ranging from 0 to 1.</param>
        /// <returns>The mixer volume.</returns>
        private float NormalizedToMixerVolume(float normalizedVolume)
        {
            return (normalizedVolume - 1) * 80;
        }

        /// <summary>
        /// Starts playing an audio clip.
        /// </summary>
        /// <param name="clip">The clip to play.</param>
        /// <returns>The audio player responsible for playing the clip.</returns>
        public AudioPlayer Play(AudioClipSO clip)
        {
            if (!_subscribedToOnFinishedPlaying)
            {
                _onFinishedPlayingChannel.OnRaised += OnFinishedPlaying;
                _subscribedToOnFinishedPlaying = true;
            }

            AudioPlayer audioPlayer = _audioPlayerPool.Request();
            audioPlayer.Play(clip);
            return audioPlayer;
        }

        /// <summary>
        /// Starts playing an audio clip.
        /// Attachs the audio player to the given anchor.
        /// </summary>
        /// <param name="clip">The clip to play.</param>
        /// <param name="anchor">The anchor to attach the audio player to.</param>
        /// <returns>The audio player responsible for playing the clip.</returns>
        public AudioPlayer Play(AudioClipSO clip, Transform anchor)
        {
            AudioPlayer audioPlayer = Play(clip);
            audioPlayer.transform.parent = anchor;
            return audioPlayer;
        }

        /// <summary>
        /// Starts playing an audio clip.
        /// Set the audio player's position.
        /// </summary>
        /// <param name="clip">The clip to play.</param>
        /// <param name="position">The position for the audio player.</param>
        /// <returns>The audio player responsible for playing the clip.</returns>
        public AudioPlayer Play(AudioClipSO clip, Vector3 position)
        {
            AudioPlayer audioPlayer = Play(clip);
            audioPlayer.transform.position = position;
            return audioPlayer;
        }

        /// <summary>
        /// Called when an audio player is finished playing its clip.
        /// </summary>
        /// <param name="audioPlayer">The audio player.</param>
        private void OnFinishedPlaying(AudioPlayer audioPlayer)
        {
            audioPlayer.gameObject.SetActive(false);
            _audioPlayerPool.Return(audioPlayer);
        }

        /// <summary>
        /// Pauses all audio players.
        /// </summary>
        public void Pause() => _onPauseChannel.Raise();

        /// <summary>
        /// Resumes all audio players.
        /// </summary>
        public void Resume() => _onResumeChannel.Raise();
    }
}
