using System.Collections.Generic;
using UnityEngine;

namespace TimeStranded.Cameras.Test
{
    /// <summary>
    /// Used to test a follow camera.
    /// </summary>
    [AddComponentMenu("Time Stranded/Cameras/Test/Follow Camera Test")]
    [DisallowMultipleComponent]
    public class FollowCameraTest : MonoBehaviour
    {
        /// <summary>
        /// The follow camera's targets.
        /// </summary>
        [Tooltip("The follow camera's targets.")]
        [SerializeField] private List<Transform> _cameraTargets = new List<Transform>();

        /// <summary>
        /// A reference list for the follow camera's targets.
        /// </summary>
        [Tooltip("A reference list for the follow camera's targets.")]
        [SerializeField] private TransformListReferenceSO _cameraTargetsReference = null;

        private void Start()
        {
            // Add the camera's targets.
            _cameraTargetsReference.AddRange(_cameraTargets);
        }
    }
}
