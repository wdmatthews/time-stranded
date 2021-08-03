using UnityEngine;
using Toolkits.Events;

namespace Toolkits.Input
{
    /// <summary>
    /// A <see cref="ScriptableObject"/> for <see cref="SavedInputRebinding"/>s that multiple objects can reference to raise or listen to events.
    /// </summary>
    [CreateAssetMenu(fileName = "NewInputRebindingEventChannel", menuName = "Toolkits/Input/Input Rebinding Event Channel")]
    public class InputRebindingEventChannelSO : EventChannelSO<SavedInputRebinding> { }
}
