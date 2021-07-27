using UnityEngine;
using UnityEditor;

namespace Toolkits.Enums.Test.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing <see cref="ScriptableObject"/> enums in the inspector.
    /// </summary>
    [CustomEditor(typeof(TestEnumSO))]
    public class TestEnumSOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TestEnumSO testEnum = (TestEnumSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");

            if (GUILayout.Button("Log Name and Color"))
            {
                testEnum.LogNameAndColor();
            }
        }
    }
}
