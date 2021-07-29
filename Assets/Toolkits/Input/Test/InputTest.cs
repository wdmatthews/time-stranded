using UnityEngine;

namespace Toolkits.Input.Test
{
    /// <summary>
    /// Used for testing input.
    /// </summary>
    [AddComponentMenu("Toolkits/Input/Test/Test")]
    [DisallowMultipleComponent]
    public class InputTest : MonoBehaviour
    {
        /// <summary>
        /// The input manager to test.
        /// </summary>
        [Tooltip("The input manager to test.")]
        [SerializeField] private InputManagerSO _inputManager = null;

        /// <summary>
        /// The event channel to raise when an input is rebound.
        /// </summary>
        [Tooltip("The event channel to raise when an input is rebound.")]
        [SerializeField] private InputRebindingEventChannelSO _onInputRebindChannel = null;

        /// <summary>
        /// The paths that are excluded from rebind choices.
        /// </summary>
        [Tooltip("The paths that are excluded from rebind choices.")]
        [SerializeField] private string[] _excludedRebindPaths = { };

        private void Start()
        {
            // Load input rebindings.
            _inputManager.LoadInputRebindings();
            Debug.Log($"Loaded input rebindings.");

            // Subscribe to the OnInputRebinding event.
            _onInputRebindChannel.OnRaised += (SavedInputRebinding inputRebinding) =>
            {
                Debug.Log($"Rebound {inputRebinding.Action}'s {inputRebinding.OldPath} to {inputRebinding.NewPath}");
            };

            // Start rebinding the jump input.
            _inputManager.StartRebind("Jump", "<Keyboard>/space", _excludedRebindPaths);
            Debug.Log("Started rebinding Jump's <Keyboard>/space.");

            // Reset input rebindings.
            //_inputManager.ResetInputRebindings();
            //Debug.Log($"Reset input rebindings.");
        }
    }
}
