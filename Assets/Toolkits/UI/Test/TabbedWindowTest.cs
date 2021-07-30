using UnityEngine;

namespace Toolkits.UI.Test
{
    /// <summary>
    /// Used for testing <see cref="TabbedWindow"/>s.
    /// </summary>
    [AddComponentMenu("Toolkits/UI/Test/Tabbed Window Test")]
    [DisallowMultipleComponent]
    public class TabbedWindowTest : MonoBehaviour
    {
        /// <summary>
        /// The tabbed window to test.
        /// </summary>
        [Tooltip("The tabbed window to test.")]
        [SerializeField] private TabbedWindow _window = null;

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

            // Select the first tab instantly.
            _window.SelectTab(0, false);
            // Select the first tab with animation.
            //_window.SelectTab(0);
        }
    }
}
