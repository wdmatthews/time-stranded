using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="ChoiceNodeData"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewChoiceEventChannel", menuName = "Time Stranded/Dialogues/Choice Event Channel")]
    public class ChoiceEventChannelSO : EventChannelSO<ChoiceNodeData> { }
}
