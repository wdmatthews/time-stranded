using UnityEngine;

namespace Toolkits.Input
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="SavedInputRebinding"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewInputRebindingEventChannel", menuName = "Toolkits/Input/Input Rebinding Event Channel")]
    public class InputRebindingEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<SavedInputRebinding> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        public void Raise(SavedInputRebinding inputRebinding) => OnRaised?.Invoke(inputRebinding);
    }
}
