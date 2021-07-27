using UnityEngine;
using UnityEditor;

namespace Toolkits.ObjectPooling.Test.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing a test factory in the inspector.
    /// </summary>
    [CustomEditor(typeof(TestFactorySO))]
    public class TestFactorySOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            TestFactorySO factory = (TestFactorySO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            if (GUILayout.Button("Create")) factory.Create();
        }
    }
}

