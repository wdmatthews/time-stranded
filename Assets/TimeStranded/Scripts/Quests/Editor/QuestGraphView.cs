using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;

namespace TimeStranded.Quests.Editor
{
    /// <summary>
    /// The <see cref="GraphView"/> for editing quests.
    /// </summary>
    public class QuestGraphView : BaseGraphView
    {
        /// <summary>
        /// Creates the <see cref="GraphView"/>, passing in the style sheet to use.
        /// </summary>
        /// <param name="styleSheet">The style sheet for the graph view.</param>
        public QuestGraphView(StyleSheet styleSheet)
            : base(styleSheet) { }

        /// <summary>
        /// Adds a quest node at the given position.
        /// </summary>
        /// <param name="position">The position to place the node.</param>
        /// <param name="data">The node's data.</param>
        /// <returns>Returns the added node.</returns>
        public QuestNode AddQuestNode(Vector2 position, QuestNodeData data = null)
        {
            QuestNode node = new QuestNode(RemoveElement, _styleSheet, position, data);
            AddElement(node);
            return node;
        }

        /// <summary>
        /// Adds a quest node from node data.
        /// </summary>
        /// <param name="data">The node's data.</param>
        /// <returns></returns>
        public QuestNode AddQuestNode(QuestNodeData data)
        {
            QuestNode node = AddQuestNode(data.Position, data);
            node.Guid = data.Guid;
            return node;
        }
    }
}
