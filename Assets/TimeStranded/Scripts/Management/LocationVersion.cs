using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Management
{
    /// <summary>
    /// Provides basic data and methods needed across most locations.
    /// </summary>
    public abstract class LocationVersion : MonoBehaviour
    {
        /// <summary>
        /// The player's data.
        /// </summary>
        [Tooltip("The player's data.")]
        [SerializeField] protected CharacterSO _playerData = null;

        /// <summary>
        /// The camera follow targets.
        /// </summary>
        [Tooltip("The camera follow targets.")]
        [SerializeField] private TransformListReferenceSO _cameraFollowTargets = null;

        /// <summary>
        /// The player instance.
        /// </summary>
        protected Character _player = null;

        private void Awake()
        {
            // Spawn the player.
            _player = Instantiate(_playerData.Prefab);
            _player.Initialize(_playerData);
            _cameraFollowTargets.Add(_player.transform);
        }
    }
}
