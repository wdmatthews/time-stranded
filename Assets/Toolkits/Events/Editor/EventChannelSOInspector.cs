using UnityEngine;
using UnityEditor;

namespace Toolkits.Events.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing events in the inspector.
    /// </summary>
    [CustomEditor(typeof(EventChannelSO))]
    public class EventChannelSOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            EventChannelSO channel = (EventChannelSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            if (GUILayout.Button("Raise")) channel.Raise();
        }
    }
}
