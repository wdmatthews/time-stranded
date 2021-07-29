using UnityEngine;
using UnityEditor;

namespace Toolkits.UI.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing the <see cref="FilledBar"/> in the inspector.
    /// </summary>
    [CustomEditor(typeof(FilledBar))]
    public class FilledBarInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The bar fill amount.
        /// </summary>
        private float _fillAmount = 1;

        public override void OnInspectorGUI()
        {
            FilledBar filledBar = (FilledBar)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            EditorGUI.BeginChangeCheck();
            _fillAmount = EditorGUILayout.Slider("Fill Amount", _fillAmount, 0, 1);
            if (EditorGUI.EndChangeCheck()) filledBar.SetFill(_fillAmount);
        }
    }
}
