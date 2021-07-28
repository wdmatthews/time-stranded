using UnityEngine;

namespace Toolkits.EventSystem
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEventChannel", menuName = "Toolkits/Event System/Event Channel")]
    public class EventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        public void Raise() => OnRaised?.Invoke();
    }
}
