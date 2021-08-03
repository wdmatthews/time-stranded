using UnityEngine;
using TimeStranded.Inventory;

namespace TimeStranded.Games
{
    /// <summary>
	/// Stores information about a ball.
	/// </summary>
    [CreateAssetMenu(fileName = "NewBall", menuName = "Time Stranded/Games/Ball")]
    public class BallSO : ItemSO
    {
        /// <summary>
        /// The move speed for the ball.
        /// </summary>
        [Tooltip("The move speed for the ball.")]
        public float MoveSpeed = 1;

        /// <summary>
        /// The friction percentage applied to the ball while moving.
        /// </summary>
        [Tooltip("The friction percentage applied to the ball while moving.")]
        [Range(0, 1)] public float Friction = 0.95f;

        /// <summary>
        /// The squared speed at which the ball should stop moving.
        /// </summary>
        [Tooltip("The squared speed at which the ball should stop moving.")]
        public float EffectivelyZeroSpeed = 0.01f;

        /// <summary>
        /// The physics material to use for the ball.
        /// </summary>
        [Tooltip("The physics material to use for the ball.")]
        public PhysicsMaterial2D PhysicsMaterial = null;
    }
}
