using UnityEngine;
using Toolkits.Saving;

namespace Toolkits.Input
{
    /// <summary>
    /// A save manager for input rebindings.
    /// </summary>
    [CreateAssetMenu(fileName = "InputRebindingsSaveManager", menuName = "Toolkits/Input/Input Rebindings Save Manager")]
    public class InputRebindingsSaveManager : SaveManagerSO<SavedInputRebindings> { }
}
