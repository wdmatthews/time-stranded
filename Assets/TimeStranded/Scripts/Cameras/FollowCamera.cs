using UnityEngine;

namespace TimeStranded.Cameras
{
    /// <summary>
    /// Makes a camera follow the given target or targets.
    /// </summary>
    [AddComponentMenu("Time Stranded/Cameras/Follow Camera")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class FollowCamera : MonoBehaviour
    {
        /// <summary>
        /// The camera used.
        /// </summary>
        [Tooltip("The camera used.")]
        [SerializeField] private Camera _camera = null;

        /// <summary>
        /// The targets to follow.
        /// </summary>
        [Tooltip("The targets to follow.")]
        [SerializeField] private TransformListReferenceSO _targets = null;

        /// <summary>
        /// The minimum camera zoom or orthographic size.
        /// </summary>
        [Tooltip("The minimum camera zoom or orthographic size.")]
        [SerializeField] private float _minZoom = 1;

        /// <summary>
        /// The maximum camera zoom or orthographic size.
        /// </summary>
        [Tooltip("The maximum camera zoom or orthographic size.")]
        [SerializeField] private float _maxZoom = 1;

        /// <summary>
        /// How much space around follow targets to give.
        /// </summary>
        [Tooltip("How much space around follow targets to give.")]
        [SerializeField] private float _followPadding = 1;

        /// <summary>
        /// The damping value for camera movement.
        /// </summary>
        [Tooltip("The damping value for camera movement.")]
        [SerializeField] [Range(0, 1)] private float _followDamping = 0.5f;

        /// <summary>
        /// The damping value for camera sizing.
        /// </summary>
        [Tooltip("The damping value for camera sizing.")]
        [SerializeField] [Range(0, 1)] private float _zoomDamping = 0.5f;

        private void Update()
        {
            int targetCount = _targets.Count;
            if (targetCount == 0) return;
            Vector2 min = new Vector2();
            Vector2 max = new Vector2();
            Vector2 center = new Vector2();

            for (int i = 0; i < targetCount; i++)
            {
                Vector3 targetPosition = _targets[i].position;
                // Calculate the min and max positions for zooming.
                if (targetPosition.x < min.x) min.x = targetPosition.x;
                if (targetPosition.x > max.x) max.x = targetPosition.x;
                if (targetPosition.y < min.y) min.y = targetPosition.y;
                if (targetPosition.y > max.y) max.y = targetPosition.y;

                // Calculate the center position of the camera.
                center += (Vector2)targetPosition;
            }

            // Used for positioning and sizing the camera.
            center /= targetCount;
            Vector3 position = transform.position;
            float aspectRatio = _camera.aspect;
            float zoom = _camera.orthographicSize;
            float neededWidth = max.x - min.x + 2 * _followPadding;
            float neededHeight = max.y - min.y + 2 * _followPadding;
            float suggestedWidth = Mathf.Clamp(neededWidth, _minZoom * aspectRatio, _maxZoom * aspectRatio);
            float suggestedHeight = Mathf.Clamp(neededHeight, _minZoom, _maxZoom);

            transform.position = Vector3.Lerp(position, new Vector3(center.x, center.y, position.z), _followDamping);

            // Pick the width or height and use aspect ratio to calculate the other,
            // using the same formula as Vector3.Lerp for consistency.
            if (suggestedWidth > suggestedHeight * aspectRatio)
            {
                _camera.orthographicSize = zoom + (suggestedWidth / aspectRatio - zoom) * _zoomDamping;
            }
            else
            {
                _camera.orthographicSize = zoom + (suggestedHeight - zoom) * _zoomDamping;
            }
        }
    }
}
