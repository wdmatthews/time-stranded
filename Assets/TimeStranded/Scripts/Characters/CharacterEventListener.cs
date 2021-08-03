using UnityEngine;
using Toolkits.Events;

namespace TimeStranded.Characters
{
    /// <summary>
    /// A component that allows for character events to be assigned in the inspector.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Character Event Listener")]
    public class CharacterEventListener : EventListener<Character> { }
}
