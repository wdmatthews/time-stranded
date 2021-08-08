using UnityEngine;
using Toolkits.References;
using TimeStranded.Characters;

namespace TimeStranded.Management
{
    /// <summary>
    /// Controls the Town behavior.
    /// </summary>
    [AddComponentMenu("Time Stranded/Management/Town")]
    [DisallowMultipleComponent]
    public class Town : LocationVersion
    {
        /// <summary>
        /// The town center spawn point.
        /// </summary>
        [Tooltip("The town center spawn point.")]
        [SerializeField] private TransformReferenceSO _townCenter = null;

        /// <summary>
        /// The arena guard's spawn point.
        /// </summary>
        [Tooltip("The arena guard's spawn point.")]
        [SerializeField] private TransformReferenceSO _arenaGuardSpawn = null;

        /// <summary>
        /// The arena guard's data.
        /// </summary>
        [Tooltip("The arena guard's data.")]
        [SerializeField] private CharacterSO _arenaGuardData = null;

        private void Start()
        {
            // TODO Pass in a TransformReferenceSO to spawn in the player at instead.
            _player.transform.position = _townCenter.Transform.position;
            _player.Aim(_townCenter.Transform.eulerAngles);

            // Spawn the arena guard.
            StaticCharacter arenaGuard = Instantiate(_arenaGuardData.StaticPrefab, transform);
            arenaGuard.Initialize(_arenaGuardData);
            arenaGuard.transform.parent = null;
            arenaGuard.transform.position = _arenaGuardSpawn.Transform.position;
        }
    }
}
