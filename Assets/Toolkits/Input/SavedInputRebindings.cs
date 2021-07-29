using System.Collections.Generic;
using UnityEngine;

namespace Toolkits.Input
{
    /// <summary>
    /// A serializable class to store input rebindings.
    /// </summary>
    [System.Serializable]
    public class SavedInputRebindings
    {
        /// <summary>
        /// The list of saved input rebindings.
        /// </summary>
        [Tooltip("The list of saved input rebindings.")]
        public List<SavedInputRebinding> InputRebindings = new List<SavedInputRebinding>();
    }
}
