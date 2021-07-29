using UnityEngine;
using UnityEditor;

namespace Toolkits.Input.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="InputManagerSO"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(InputManagerSO))]
    public class InputManagerSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// Used in the inspector to specify the action.
        /// </summary>
        private string _action = "Jump";

        /// <summary>
        /// Used in the inspector to specify the old path.
        /// </summary>
        private string _oldPath = "<Keyboard>/space";

        /// <summary>
        /// Used in the inspector to exclude certain rebind options.
        /// </summary>
        private string[] _excludedRebindPaths = {
            "<Mouse>/position",
            "<Mouse>/delta",
            "<Keyboard>/p",
            "<Keyboard>/escape"
        };

        public override void OnInspectorGUI()
        {
            InputManagerSO inputManager = (InputManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _action = EditorGUILayout.TextField("Input Action", _action);
            _oldPath = EditorGUILayout.TextField("Old Path", _oldPath);
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Load"))
            {
                inputManager.LoadInputRebindings();
            }

            if (GUILayout.Button("Rebind"))
            {
                inputManager.StartRebind(_action, _oldPath, _excludedRebindPaths);
            }

            if (GUILayout.Button("Reset"))
            {
                inputManager.ResetInputRebindings();
            }

            GUILayout.EndHorizontal();
        }
    }
}
