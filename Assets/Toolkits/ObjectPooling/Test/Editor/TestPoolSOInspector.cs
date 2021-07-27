using UnityEngine;
using UnityEditor;

namespace Toolkits.ObjectPooling.Test.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing a test pool in the inspector.
    /// </summary>
    [CustomEditor(typeof(TestPoolSO))]
    public class TestPoolSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// Used in the inspector when the test pool needs a count integer.
        /// </summary>
        private int _count = 2;

        /// <summary>
        /// Used to store a requested instance.
        /// </summary>
        private TestComponent _instance = null;

        /// <summary>
        /// Used to store requested instances.
        /// </summary>
        private TestComponent[] _instances = null;

        public override void OnInspectorGUI()
        {
            TestPoolSO pool = (TestPoolSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _count = EditorGUILayout.IntField("Count", _count);
            if (GUILayout.Button("Prefill")) pool.Prefill(_count);

            if (GUILayout.Button("Request"))
            {
                if (_instance) pool.Return(_instance);
                _instance = pool.Request();
                _instance.OnRequest();
            }

            if (_instance && GUILayout.Button("Return"))
            {
                pool.Return(_instance);
                _instance.OnReturn();
                _instance = null;
            }

            if (GUILayout.Button("Request Multiple"))
            {
                if (_instances != null) pool.Return(_instances);
                _instances = (TestComponent[])pool.Request(_count);

                for (int i = _instances.Length - 1; i >= 0; i--)
                {
                    _instances[i].OnRequest();
                }
            }

            if (_instances != null && GUILayout.Button("Return Multiple"))
            {
                pool.Return(_instances);

                for (int i = _instances.Length - 1; i >= 0; i--)
                {
                    _instances[i].OnReturn();
                }

                _instances = null;
            }
        }
    }
}

