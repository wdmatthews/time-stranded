using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;

namespace TimeStranded.Dialogues.Editor
{
    /// <summary>
    /// The <see cref="GraphView"/> for editing quests.
    /// </summary>
    public class DialogueGraphView : BaseGraphView
    {
        /// <summary>
        /// Creates the <see cref="GraphView"/>, passing in the style sheet to use.
        /// </summary>
        /// <param name="styleSheet">The style sheet for the graph view.</param>
        public DialogueGraphView(StyleSheet styleSheet)
            : base(styleSheet) { }

        /// <summary>
        /// Adds a message node at the given position.
        /// </summary>
        /// <param name="position">The position to place the node.</param>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public MessageNode AddMessageNode(Vector2 position, MessageNodeData data = null)
        {
            MessageNode node = new MessageNode(RemoveElement, _styleSheet, position, data);
            AddElement(node);
            return node;
        }

        /// <summary>
        /// Adds a message node from node data.
        /// </summary>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public MessageNode AddMessageNode(MessageNodeData data)
        {
            MessageNode node = AddMessageNode(data.Position, data);
            node.Guid = data.Guid;
            return node;
        }

        /// <summary>
        /// Adds a choice node at the given position.
        /// </summary>
        /// <param name="position">The position to place the node.</param>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public ChoiceNode AddChoiceNode(Vector2 position, ChoiceNodeData data = null)
        {
            ChoiceNode node = new ChoiceNode(RemoveElement, _styleSheet, position, data);
            AddElement(node);
            return node;
        }

        /// <summary>
        /// Adds a choice node from node data.
        /// </summary>
        /// <param name="data">The node's data.</param>
        /// <returns>The added node.</returns>
        public ChoiceNode AddChoiceNode(ChoiceNodeData data)
        {
            ChoiceNode node = AddChoiceNode(data.Position, data);
            node.Guid = data.Guid;
            return node;
        }
    }
}
