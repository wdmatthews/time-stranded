using UnityEngine;
using UnityEditor;

namespace TimeStranded.UI.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing <see cref="ThemeManagerSO"/>s in the inspector.
    /// </summary>
    [CustomEditor(typeof(ThemeManagerSO))]
    public class ThemeManagerSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The theme to apply.
        /// </summary>
        private ThemeSO _theme = null;

        public override void OnInspectorGUI()
        {
            ThemeManagerSO themeManager = (ThemeManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _theme = (ThemeSO)EditorGUILayout.ObjectField("Theme", _theme, typeof(ThemeSO), false);
            if (GUILayout.Button("Apply")) themeManager.ApplyTheme(_theme.name);
        }
    }
}
