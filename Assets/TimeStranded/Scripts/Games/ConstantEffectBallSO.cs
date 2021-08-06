using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
	/// Stores information about a constant effect ball.
	/// </summary>
    [CreateAssetMenu(fileName = "NewConstantEffectBall", menuName = "Time Stranded/Games/Constant Effect Ball")]
    public class ConstantEffectBallSO : BallSO
    {
        /// <summary>
        /// The name of the attribute to affect on pickup and release.
        /// </summary>
        [Tooltip("The name of the attribute to affect on pickup and release.")]
        public string AttributeName = "";

        /// <summary>
        /// The value to change the effect by each second.
        /// </summary>
        [Tooltip("The value to change the effect by each second.")]
        public float EffectValue = 1;

        /// <summary>
        /// Apply the effect.
        /// </summary>
        /// <param name="character">The character to apply the affect to.</param>
        public override void OnUpdate(Character character)
        {
            float amount = Time.deltaTime * EffectValue;

            // Make sure to use the Heal or TakeDamage methods if needed.
            if (AttributeName == "Health")
            {
                if (EffectValue >= 0) character.Heal(amount);
                else character.TakeDamage(-amount);
            }
            else character.AttributesByName[AttributeName].ChangeValue(amount);
        }
    }
}
