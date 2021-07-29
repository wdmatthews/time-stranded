using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Toolkits.Input
{
    /// <summary>
    /// Manages input rebinding.
    /// </summary>
    [CreateAssetMenu(fileName = "NewInputManager", menuName = "Toolkits/Input/Input Manager")]
    public class InputManagerSO : ScriptableObject
    {
        /// <summary>
        /// The input actions to use for this manager.
        /// </summary>
        [Tooltip("The input actions to use for this manager.")]
        [SerializeField] private InputActionAsset _inputActions = null;

        /// <summary>
        /// The event channel to raise when an input is rebound.
        /// </summary>
        [Tooltip("The event channel to raise when an input is rebound.")]
        [SerializeField] private InputRebindingEventChannelSO _onInputRebindChannel = null;

        /// <summary>
        /// The save manager to save input rebindings.
        /// </summary>
        [Tooltip("The save manager to save input rebindings.")]
        [SerializeField] private InputRebindingsSaveManager _saveManager = null;

        /// <summary>
        /// The save name for the input rebindings.
        /// </summary>
        [Tooltip("The save name for the input rebindings.")]
        [SerializeField] private string _inputRebindingsSaveName = "";

        /// <summary>
        /// The saved input rebindings.
        /// </summary>
        [System.NonSerialized] private SavedInputRebindings _inputRebindings = new SavedInputRebindings();

        /// <summary>
        /// Loads the saved input rebindings into the input actions.
        /// </summary>
        public void LoadInputRebindings()
        {
            // Load the input rebindings save file.
            SavedInputRebindings inputRebindings = _saveManager.Load(_inputRebindingsSaveName);
            if (inputRebindings == null) return;
            _inputRebindings = inputRebindings;
            List<SavedInputRebinding> rebindings = _inputRebindings.InputRebindings;

            for (int i = rebindings.Count - 1; i >= 0; i--)
            {
                // Get the corresponding input action and apply the binding override.
                SavedInputRebinding inputRebinding = rebindings[i];
                _inputActions.FindAction(inputRebinding.Action)
                    .ApplyBindingOverride(new InputBinding
                    {
                        path = inputRebinding.OldPath,
                        overridePath = inputRebinding.NewPath
                    });
            }
        }

        /// <summary>
        /// Saves the input rebindings.
        /// </summary>
        private void SaveInputRebindings()
        {
            _saveManager.Save(_inputRebindingsSaveName, _inputRebindings);
        }

        /// <summary>
        /// Removes all input rebindings and deletes the save for it.
        /// </summary>
        public void ResetInputRebindings()
        {
            List<SavedInputRebinding> rebindings = _inputRebindings.InputRebindings;

            for (int i = rebindings.Count - 1; i >= 0; i--)
            {
                // Get the corresponding input action and remove the binding override.
                SavedInputRebinding inputRebinding = rebindings[i];
                _inputActions.FindAction(inputRebinding.Action)
                    .RemoveBindingOverride(new InputBinding
                    {
                        path = inputRebinding.OldPath,
                        overridePath = inputRebinding.NewPath
                    });
            }

            // Delete the input rebindings save file.
            _saveManager.DeleteSave(_inputRebindingsSaveName);
        }

        /// <summary>
        /// Starts an interactive rebind.
        /// </summary>
        /// <param name="action">The action containing the path to rebind.</param>
        /// <param name="path">The path to rebind.</param>
        /// <param name="excludedPaths">The paths to exclude from rebinding choices, such as &lt;Mouse&gt;/position.</param>
        public void StartRebind(string action, string path, string[] excludedPaths)
        {
            // Prepare the interactive rebinding.
            InputAction inputAction = _inputActions.FindAction(action);
            var rebind = inputAction.PerformInteractiveRebinding(
                inputAction.GetBindingIndex(new InputBinding { path = path })
            );

            // Exclude certain paths, such as <Mouse>/position,
            // to prevent mouse movement from making the player jump.
            for (int i = excludedPaths.Length - 1; i >= 0; i--)
            {
                rebind.WithControlsExcluding(excludedPaths[i]);
            }

            rebind.OnComplete(operation =>
            {
                string newPath = operation.selectedControl.path;
                SavedInputRebinding inputRebinding = new SavedInputRebinding(action, path, newPath);
                List<SavedInputRebinding> rebindings = _inputRebindings.InputRebindings;
                bool updatedRebinding = false;

                for (int i = rebindings.Count - 1; i >= 0; i--)
                {
                    // Get the corresponding input action and update the binding override if needed.
                    SavedInputRebinding rebinding = rebindings[i];

                    if (rebinding.Action == action && rebinding.OldPath == path)
                    {
                        rebinding.NewPath = newPath;
                        updatedRebinding = true;
                        break;
                    }
                }

                // Add the input rebinding if it is new.
                if (!updatedRebinding) _inputRebindings.InputRebindings.Add(inputRebinding);
                // Save the rebindings.
                SaveInputRebindings();
                // Notify any listeners of the rebinding.
                _onInputRebindChannel.Raise(inputRebinding);
                // Prevent a memory leak by disposing the rebinding operation.
                operation.Dispose();
            });

            // Start the rebinding.
            rebind.Start();
        }
    }
}
