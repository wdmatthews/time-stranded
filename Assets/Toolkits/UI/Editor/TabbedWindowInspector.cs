using UnityEngine;
using UnityEditor;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="TabbedWindow"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(TabbedWindow))]
    [CanEditMultipleObjects]
    public class TabbedWindowInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The index of the tab to select.
        /// </summary>
        private int _tabIndex = 0;

        public override void OnInspectorGUI()
        {
            int targetCount = targets.Length;
            TabbedWindow[] windows = new TabbedWindow[targetCount];

            for (int i = 0; i < targetCount; i++)
            {
                windows[i] = (TabbedWindow)targets[i];
            }

            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].Open();
                }
            }
            else if (GUILayout.Button("Close"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].Close();
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Open Instantly"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].Open(false);
                }
            }
            else if (GUILayout.Button("Close Instantly"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].Close(false);
                }
            }

            GUILayout.EndHorizontal();
            EditorGUI.BeginChangeCheck();
            _tabIndex = EditorGUILayout.IntField("Tab Index", _tabIndex);

            if (EditorGUI.EndChangeCheck())
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].SelectTab(_tabIndex);
                }
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Select Previous"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].SelectPreviousTab();
                }
            }
            else if (GUILayout.Button("Select Next"))
            {
                for (int i = 0; i < targetCount; i++)
                {
                    windows[i].SelectNextTab();
                }
            }

            GUILayout.EndHorizontal();
        }
    }
}
