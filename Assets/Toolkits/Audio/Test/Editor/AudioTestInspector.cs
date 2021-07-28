using UnityEngine;
using UnityEditor;

namespace Toolkits.Audio.Test.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing audio in the inspector.
    /// </summary>
    [CustomEditor(typeof(AudioTest))]
    public class AudioManagerSOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            AudioTest audioTest = (AudioTest)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            if (GUILayout.Button("Play Music")) audioTest.PlayMusicClip();
            if (GUILayout.Button("Play Sound Effect")) audioTest.PlaySFXClip();
        }
    }
}
