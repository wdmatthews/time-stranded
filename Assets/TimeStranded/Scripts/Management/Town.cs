using UnityEngine;
using Toolkits.References;

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

        private void Start()
        {
            // TODO Pass in a TransformReferenceSO to spawn in the player at instead.
            _player.transform.position = _townCenter.Transform.position;
        }
    }
}
