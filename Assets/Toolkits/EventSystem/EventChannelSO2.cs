using UnityEngine;

namespace Toolkits.EventSystem
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> that multiple objects can reference to raise or listen to events.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public abstract class EventChannelSO<T1, T2> : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<T1, T2> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="parameter1"></param>
        /// <param name="parameter2"></param>
        public void Raise(T1 parameter1, T2 parameter2) => OnRaised?.Invoke(parameter1, parameter2);
    }
}
