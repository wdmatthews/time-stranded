using UnityEngine;
using UnityEditor;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="Window"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(Window))]
    [CanEditMultipleObjects]
    public class WindowInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            int targetCount = targets.Length;
            Window[] windows = new Window[targetCount];

            for (int i = 0; i < targetCount; i++)
            {
                windows[i] = (Window)targets[i];
            }

            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open"))
            {
                for (int i = windows.Length - 1; i >= 0; i--)
                {
                    windows[i].Open();
                }
            }
            else if (GUILayout.Button("Close"))
            {
                for (int i = windows.Length - 1; i >= 0; i--)
                {
                    windows[i].Close();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open Instantly"))
            {
                for (int i = windows.Length - 1; i >= 0; i--)
                {
                    windows[i].Open(false);
                }
            }
            else if (GUILayout.Button("Close Instantly"))
            {
                for (int i = windows.Length - 1; i >= 0; i--)
                {
                    windows[i].Close(false);
                }
            }

            GUILayout.EndHorizontal();
        }
    }
}
