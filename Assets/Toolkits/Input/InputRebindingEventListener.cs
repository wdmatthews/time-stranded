using UnityEngine;
using Toolkits.EventSystem;

namespace Toolkits.Input
{
    /// <summary>
    /// A component that allows for input rebinding events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Toolkits/Input/Input Rebinding Event Listener")]
    public class InputRebindingEventListener : EventListener<SavedInputRebinding> { }
}
