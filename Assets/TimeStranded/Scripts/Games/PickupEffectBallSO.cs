using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
	/// Stores information about a pickup effect ball.
	/// </summary>
    [CreateAssetMenu(fileName = "NewPickupEffectBall", menuName = "Time Stranded/Games/Pickup Effect Ball")]
    public class PickupEffectBallSO : BallSO
    {
        /// <summary>
        /// The name of the attribute to affect on pickup and release.
        /// </summary>
        [Tooltip("The name of the attribute to affect on pickup and release.")]
        public string AttributeName = "";

        /// <summary>
        /// The value to change the effect by.
        /// </summary>
        [Tooltip("The value to change the effect by.")]
        public float EffectValue = 1;

        /// <summary>
        /// Apply the effect.
        /// </summary>
        /// <param name="character">The character to apply the affect to.</param>
        public override void OnPickup(Character character)
        {
            character.AttributesByName[AttributeName].ChangeValue(EffectValue);
        }

        /// <summary>
        /// Remove the effect.
        /// </summary>
        /// <param name="character">The character to remove the affect from.</param>
        public override void OnRelease(Character character)
        {
            character.AttributesByName[AttributeName].ChangeValue(-EffectValue);
        }
    }
}
