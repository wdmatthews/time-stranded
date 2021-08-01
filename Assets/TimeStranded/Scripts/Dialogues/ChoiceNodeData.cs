using System.Collections.Generic;
using Toolkits.NodeEditor;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// Stores data about a choice node.
    /// </summary>
    [System.Serializable]
    public class ChoiceNodeData : BaseNodeData
    {
        /// <summary>
        /// The list of message ids to pick from.
        /// </summary>
        public List<string> Choices = new List<string>();

        /// <summary>
        /// Creates empty node data.
        /// </summary>
        public ChoiceNodeData() { }

        /// <summary>
        /// Creates node data from the given data.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public ChoiceNodeData(ChoiceNodeData data)
            : base(data)
        {
            Choices = new List<string>(data.Choices);
        }
    }
}
