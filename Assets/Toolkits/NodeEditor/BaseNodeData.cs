using UnityEngine;

namespace Toolkits.NodeEditor
{
    /// <summary>
    /// Stores the data needed to create a base node.
    /// </summary>
    [System.Serializable]
    public class BaseNodeData
    {
        /// <summary>
        /// The Guid of the node.
        /// </summary>
        public string Guid = "";

        /// <summary>
        /// The position of the node.
        /// </summary>
        public Vector2 Position = new Vector2();

        /// <summary>
        /// Creates empty node data.
        /// </summary>
        public BaseNodeData() { }

        /// <summary>
        /// Creates node data from the given data.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public BaseNodeData(BaseNodeData data)
        {
            Guid = data.Guid;
            Position = data.Position;
        }
    }
}
