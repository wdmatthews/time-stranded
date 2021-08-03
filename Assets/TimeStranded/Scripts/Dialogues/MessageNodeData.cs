using Toolkits.NodeEditor;
using TimeStranded.Characters;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// Stores data about a message node.
    /// </summary>
    [System.Serializable]
    public class MessageNodeData : BaseNodeData
    {
        /// <summary>
        /// The character saying this message.
        /// </summary>
        public CharacterSO Speaker = null;

        /// <summary>
        /// The content of the message.
        /// </summary>
        public string Content = "";

        /// <summary>
        /// Whether or not this message node is the starting message.
        /// </summary>
        public bool IsStartMessage = false;

        /// <summary>
        /// The node id for the next message or choice.
        /// </summary>
        public string NextId = "";

        /// <summary>
        /// Whether the next node is a message or a choice.
        /// </summary>
        public bool NextIsMessage = true;

        /// <summary>
        /// Creates empty node data.
        /// </summary>
        public MessageNodeData() { }

        /// <summary>
        /// Creates node data from the given data.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public MessageNodeData(MessageNodeData data)
            : base(data)
        {
            Speaker = data.Speaker;
            Content = data.Content;
            IsStartMessage = data.IsStartMessage;
            NextId = data.NextId;
            NextIsMessage = data.NextIsMessage;
        }
    }
}
