using System.Collections;
using UnityEngine;
using DG.Tweening;
using Toolkits.EventSystem;

namespace Toolkits.Audio
{
    /// <summary>
    /// A component that plays audio.
    /// </summary>
    [AddComponentMenu("Toolkits/Audio/Audio Player")]
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        /// <summary>
        /// The audio source to play from.
        /// </summary>
        [Tooltip("The audio source to play from.")]
        [SerializeField] private AudioSource _audioSource = null;

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
        /// The time it takes to fade out.
        /// </summary>
        [Tooltip("The time it takes to fade out.")]
        [SerializeField] private float _fadeOutTime = 1;

        /// <summary>
        /// The time it takes to fade in.
        /// </summary>
        [Tooltip("The time it takes to fade out.")]
        [SerializeField] private float _fadeInTime = 1;

        /// <summary>
        /// Whether or not the audio player is playing.
        /// </summary>
        public bool IsPlaying => _audioSource.isPlaying;

        private void OnEnable()
        {
            _onPauseChannel.OnRaised += Pause;
            _onResumeChannel.OnRaised += Resume;
        }

        private void OnDisable()
        {
            _onPauseChannel.OnRaised -= Pause;
            _onResumeChannel.OnRaised -= Resume;
        }

        /// <summary>
        /// Plays an audio clip.
        /// </summary>
        /// <param name="clip">The clip to play.</param>
        public void Play(AudioClipSO clip)
        {
            gameObject.SetActive(true);

            // Set AudioSource settings.
            SetClip(clip);
            _audioSource.volume = Random.Range(clip.MinVolume, clip.MaxVolume);
            _audioSource.loop = clip.ShouldLoop;

            // Play the audio clip and wait for it to finish if it does not loop.
            _audioSource.Play();
            if (!clip.ShouldLoop) StartCoroutine(WaitUntilFinished(clip.Clip.length));
        }

        /// <summary>
        /// Sets the current audio clip.
        /// </summary>
        /// <param name="clip">The clip to play.</param>
        private void SetClip(AudioClipSO clip)
        {
            _audioSource.clip = clip.Clip;
            _audioSource.pitch = Random.Range(clip.MinPitch, clip.MaxPitch);
            _audioSource.time = 0;
            _audioSource.outputAudioMixerGroup = clip.Group.MixerGroup;
        }

        /// <summary>
        /// Fades the audio player to play a new clip.
        /// </summary>
        /// <param name="newClip">The new clip to play.</param>
        public void FadeToClip(AudioClipSO newClip)
        {
            var fadeSequence = DOTween.Sequence();
            fadeSequence.Append(_audioSource.DOFade(0, _fadeOutTime));
            fadeSequence.AppendCallback(() => {
                SetClip(newClip);
                _audioSource.Play();
            });
            fadeSequence.Append(_audioSource.DOFade(Random.Range(newClip.MinVolume, newClip.MaxVolume), _fadeInTime));
            fadeSequence.Play();
        }

        /// <summary>
        /// A coroutine that waits for the audio clip to finish playing.
        /// </summary>
        /// <param name="time">The length of the audio clip.</param>
        /// <returns></returns>
        private IEnumerator WaitUntilFinished(float time)
        {
            yield return new WaitForSeconds(time);
            _onFinishedPlayingChannel.Raise(this);
        }

        /// <summary>
        /// Pauses the audio player.
        /// </summary>
        public void Pause() => _audioSource.Pause();

        /// <summary>
        /// Resumes the audio player.
        /// </summary>
        public void Resume() => _audioSource.Play();
    }
}
