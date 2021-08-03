using UnityEngine;
using Toolkits.EventSystem;

namespace Toolkits.Audio
{
    /// <summary>
    /// A component that allows for audio player events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Toolkits/Audio/Audio Player Event Listener")]
    public class AudioPlayerEventListener : EventListener<AudioPlayer> { }
}
