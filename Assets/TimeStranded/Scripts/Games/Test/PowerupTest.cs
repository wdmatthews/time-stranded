using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games.Test
{
    /// <summary>
	/// Used to test powerups.
	/// </summary>
    [AddComponentMenu("Time Stranded/Games/Test/Powerup Test")]
    [DisallowMultipleComponent]
    public class PowerupTest : MonoBehaviour
    {
        /// <summary>
        /// The character to test the powerup on.
        /// </summary>
        [Tooltip("The character to test the powerup on.")]
        [SerializeField] private Character _character = null;

        /// <summary>
        /// The powerup to test.
        /// </summary>
        [Tooltip("The powerup to test.")]
        [SerializeField] private Powerup _powerup = null;

        private void Start()
        {
            // Make the character hold the powerup.
            _character.HoldItem(_powerup);
        }
    }
}
