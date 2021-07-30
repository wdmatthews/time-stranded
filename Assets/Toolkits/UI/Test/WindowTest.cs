using UnityEngine;

namespace Toolkits.UI.Test
{
    /// <summary>
    /// Used for testing <see cref="Window"/>s.
    /// </summary>
    [AddComponentMenu("Toolkits/UI/Test/Window Test")]
    [DisallowMultipleComponent]
    public class WindowTest : MonoBehaviour
    {
        /// <summary>
        /// The window to test.
        /// </summary>
        [Tooltip("The window to test.")]
        [SerializeField] private Window _window = null;

        private void Start()
        {
            // Open the window instantly.
            _window.Open(false);
            // Open the window with animation.
            //_window.Open();
            // Close the window instantly.
            //_window.Close(false);
            // Close the window with animation.
            //_window.Close();
        }
    }
}
