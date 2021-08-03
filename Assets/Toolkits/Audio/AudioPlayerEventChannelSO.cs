using UnityEngine;
using Toolkits.EventSystem;

namespace Toolkits.Audio
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioPlayerEventChannel", menuName = "Toolkits/Audio/Audio Player Event Channel")]
    public class AudioPlayerEventChannelSO : EventChannelSO<AudioPlayer> { }
}
