namespace Toolkits.Input
{
    /// <summary>
    /// Represents a single input rebinding.
    /// </summary>
    [System.Serializable]
    public class SavedInputRebinding
    {
        /// <summary>
        /// The action this input rebinding applies to.
        /// </summary>
        public string Action = "";

        /// <summary>
        /// The input path as defined in the input actions.
        /// </summary>
        public string OldPath = "";

        /// <summary>
        /// The input path as defined by the user.
        /// </summary>
        public string NewPath = "";

        /// <summary>
        /// Creates an empty <see cref="SavedInputRebinding"/>.
        /// </summary>
        public SavedInputRebinding() { }

        /// <summary>
        /// Creates a <see cref="SavedInputRebinding"/> with the given action, old path, and new path.
        /// </summary>
        /// <param name="action">The action with the path to rebind.</param>
        /// <param name="oldPath">The path to rebind.</param>
        /// <param name="newPath">The path after the rebind.</param>
        public SavedInputRebinding(string action, string oldPath, string newPath)
        {
            Action = action;
            OldPath = oldPath;
            NewPath = newPath;
        }
    }
}
