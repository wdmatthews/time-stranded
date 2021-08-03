using UnityEngine;

namespace Toolkits.Events
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that multiple objects can reference to raise or listen to events.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public abstract class EventChannelSO<T1> : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<T1> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="parameter1"></param>
        public void Raise(T1 parameter1) => OnRaised?.Invoke(parameter1);
    }
}
