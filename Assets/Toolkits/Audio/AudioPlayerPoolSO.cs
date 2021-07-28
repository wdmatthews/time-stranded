using UnityEngine;
using Toolkits.ObjectPooling;

namespace Toolkits.Audio
{
    /// <summary>
    /// Used to pool <see cref="AudioPlayer"/>s.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAudioPlayerPool", menuName = "Toolkits/Audio/Audio Player Pool")]
    public class AudioPlayerPoolSO : PoolSO<AudioPlayer>
    {
        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        [Tooltip("The factory to use when creating instances.")]
        [SerializeField] private AudioPlayerFactorySO _factory = null;

        /// <summary>
        /// The factory to use when creating instances.
        /// </summary>
        protected override IFactory<AudioPlayer> Factory => _factory;
    }
}
