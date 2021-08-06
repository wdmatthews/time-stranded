using UnityEngine;

namespace TimeStranded.Games
{
    /// <summary>
    /// Stores data needed for a phase wall.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPhaseWall", menuName = "Time Stranded/Games/Phase Wall")]
    public class PhaseWallSO : ScriptableObject
    {
        /// <summary>
        /// The alpha color when phased out.
        /// </summary>
        [Tooltip("The alpha color when phased out.")]
        [Range(0, 1)] public float PhaseOutAlpha = 0.5f;

        /// <summary>
        /// The duration of the phase in animation.
        /// </summary>
        [Tooltip("The duration of the phase in animation.")]
        public float PhaseInDuration = 1;

        /// <summary>
        /// The duration of the phase out animation.
        /// </summary>
        [Tooltip("The duration of the phase out animation.")]
        public float PhaseOutDuration = 1;

        /// <summary>
        /// The minimum time between phasing in and out.
        /// </summary>
        [Tooltip("The minimum time between phasing in and out.")]
        public float MinCooldownDuration = 1;

        /// <summary>
        /// The maximum time between phasing in and out.
        /// </summary>
        [Tooltip("The maximum time between phasing in and out.")]
        public float MaxCooldownDuration = 1;
    }
}
