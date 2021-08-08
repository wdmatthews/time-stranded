using UnityEngine;
using UnityEngine.Events;
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
        [Tooltip("The character saying this message.")]
        public CharacterSO Speaker = null;

        /// <summary>
        /// The content of the message.
        /// </summary>
        [Tooltip("The content of the message.")]
        [TextArea] public string Content = "";

        /// <summary>
        /// The content after being formatted.
        /// </summary>
        [System.NonSerialized] public string FormattedContent = "";

        /// <summary>
        /// Whether or not this message node is the starting message.
        /// </summary>
        [Tooltip("Whether or not this message node is the starting message.")]
        public bool IsStartMessage = false;

        /// <summary>
        /// The node id for the next message or choice.
        /// </summary>
        [Tooltip("The node id for the next message or choice.")]
        public string NextId = "";

        /// <summary>
        /// Whether the next node is a message or a choice.
        /// </summary>
        [Tooltip("Whether the next node is a message or a choice.")]
        public bool NextIsMessage = true;

        /// <summary>
        /// An event that is invoked when the dialogue reaches this message.
        /// </summary>
        [Tooltip("An event that is invoked when the dialogue reaches this message.")]
        public UnityEvent Event = null;

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
