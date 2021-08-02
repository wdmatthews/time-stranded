using UnityEngine;
using TimeStranded.Characters;

namespace TimeStranded.Games
{
    /// <summary>
    /// Handles ball controls for a character.
    /// </summary>
    [AddComponentMenu("Time Stranded/Games/Ball Handler")]
    [DisallowMultipleComponent]
    public class BallHandler : MonoBehaviour
    {
        /// <summary>
        /// The character to control the ball with.
        /// </summary>
        [Tooltip("The character to control the ball with.")]
        [SerializeField] private Character _character = null;

        /// <summary>
        /// The name of the ball layer.
        /// </summary>
        [Tooltip("The name of the ball layer.")]
        [SerializeField] private string _ballLayerName = "Ball";

        /// <summary>
        /// The ball layer.
        /// </summary>
        private int _ballLayer = 0;

        private void Awake()
        {
            // Get the ball layer.
            _ballLayer = LayerMask.NameToLayer(_ballLayerName);
        }

        /// <summary>
        /// Tries to automatically pick up a ball when colliding with it.
        /// </summary>
        /// <param name="collider"></param>
        private void OnTriggerEnter2D(Collider2D collider)
        {
            // If the object is a ball, try to pick it up.
            if (collider.gameObject.layer == _ballLayer)
            {
                Ball ball = collider.gameObject.GetComponent<Ball>();
                if (ball.CanBePickedUp && !ball.IsBeingHeld) _character.HoldItem(ball);
            }
        }
    }
}
