using UnityEngine;
using TimeStranded.Inventory;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Used to detect damage hits, such as from a dodgeball.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Hit Detector")]
    [DisallowMultipleComponent]
    public class HitDetector : MonoBehaviour
    {
        /// <summary>
        /// The character affected by the hit.
        /// </summary>
        [Tooltip("The character affected by the hit.")]
        [SerializeField] private Character _character = null;

        /// <summary>
        /// The names of each layer to detect.
        /// </summary>
        [Tooltip("The names of each layer to detect.")]
        [SerializeField] private string[] _layerNames = { };

        /// <summary>
        /// The layers to detect.
        /// </summary>
        private int[] _layers = { };

        /// <summary>
        /// The number of layers.
        /// </summary>
        private int _layerCount = 0;

        private void Awake()
        {
            // Get the layers.
            _layerCount = _layerNames.Length;
            _layers = new int[_layerCount];

            for (int i = 0; i < _layerCount; i++)
            {
                _layers[i] = LayerMask.NameToLayer(_layerNames[i]);
            }
        }

        /// <summary>
        /// Notifies the character of being hit.
        /// </summary>
        /// <param name="collider"></param>
        private void OnTriggerEnter2D(Collider2D collider)
        {
            for (int i = 0; i < _layerCount; i++)
            {
                if (collider.gameObject.layer == _layers[i])
                {
                    // Get the item from the collider and apply its OnHit method to the character.
                    Item item = collider.gameObject.GetComponent<Item>();
                    item.OnHit(_character);
                    break;
                }
            }
        }
    }
}
