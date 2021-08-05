using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded.Games
{
    /// <summary>
    /// Manages an arena map.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games")]
    [DisallowMultipleComponent]
    public class ArenaMap : MonoBehaviour
    {
        /// <summary>
        /// The spawns used for characters spawning in.
        /// </summary>
        [Tooltip("The spawns used for characters spawning in.")]
        public List<Transform> Spawns = new List<Transform>();
    }
}
