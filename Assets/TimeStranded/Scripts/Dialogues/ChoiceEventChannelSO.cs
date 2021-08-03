using UnityEngine;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="ChoiceNodeData"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewChoiceEventChannel", menuName = "Time Stranded/Dialogues/Choice Event Channel")]
    public class ChoiceEventChannelSO : ScriptableObject
    {
        /// <summary>
        /// The action to invoke upon raise.
        /// </summary>
        public System.Action<ChoiceNodeData> OnRaised = null;

        /// <summary>
        /// Raises the event, notifying all listeners.
        /// </summary>
        /// <param name="choice">The choice to pass when raising the event.</param>
        public void Raise(ChoiceNodeData choice) => OnRaised?.Invoke(choice);
    }
}
