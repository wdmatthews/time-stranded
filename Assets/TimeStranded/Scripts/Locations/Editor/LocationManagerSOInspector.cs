using UnityEngine;
using UnityEditor;

namespace TimeStranded.Locations.Editor
{
    /// <summary>
    /// A custom inspector to allow for testing <see cref="LocationManagerSO"/>s in the inspector.
    /// </summary>
    [CustomEditor(typeof(LocationManagerSO))]
    public class LocationManagerSOInspector : UnityEditor.Editor
    {
        /// <summary>
        /// The location to load.
        /// </summary>
        private LocationSO _location = null;

        public override void OnInspectorGUI()
        {
            LocationManagerSO locationManager = (LocationManagerSO)target;
            base.OnInspectorGUI();

            GUILayout.Label("Play Mode Testing");
            _location = (LocationSO)EditorGUILayout.ObjectField("Location", _location, typeof(LocationSO), false);
            if (GUILayout.Button("Load")) locationManager.LoadLocation(_location);
        }
    }
}
