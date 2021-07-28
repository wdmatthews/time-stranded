using UnityEngine;
using UnityEditor;

namespace Toolkits.Audio.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing audio managers in the inspector.
    /// </summary>
    [CustomEditor(typeof(AudioManagerSO))]
    public class AudioManagerSOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            AudioManagerSO audioManager = (AudioManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
        }
    }
}
