using UnityEngine;

namespace Toolkits.Audio
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioPlayerEventChannel", menuName = "Toolkits/Audio/Audio Player Event Channel")]
    public class AudioPlayerEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<AudioPlayer> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        public void Raise(AudioPlayer audioPlayer) => OnRaised?.Invoke(audioPlayer);
    }
}
