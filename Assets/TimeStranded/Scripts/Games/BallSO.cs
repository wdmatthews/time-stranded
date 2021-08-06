using UnityEngine;
using TimeStranded.Characters;
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

        /// <summary>
        /// The maximum bounce angle offset when hitting a character.
        /// </summary>
        [Tooltip("The maximum bounce angle offset when hitting a character.")]
        public float BounceAngle = 15;

        /// <summary>
        /// The damage taken when this ball collides with a character.
        /// </summary>
        [Tooltip("The damage taken when this ball collides with a character.")]
        public float DamageOnHit = 0;

        /// <summary>
        /// Release the ball when the character holding it dies.
        /// </summary>
        public override bool ReleaseOnCharacterDeath => true;

        /// <summary>
        /// Called when the ball is picked up.
        /// </summary>
        /// <param name="character">The character that picked the ball up.</param>
        public virtual void OnPickup(Character character) { }

        /// <summary>
        /// Called when the ball is released.
        /// </summary>
        /// <param name="character">The character that released the ball.</param>
        public virtual void OnRelease(Character character) { }

        /// <summary>
        /// Called on Update.
        /// </summary>
        /// <param name="character">The character holding the ball.</param>
        public virtual void OnUpdate(Character character) { }
    }
}
