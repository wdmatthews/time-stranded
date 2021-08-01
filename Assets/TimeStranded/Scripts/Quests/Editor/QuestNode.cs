using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;

namespace TimeStranded.Quests.Editor
{
    /// <summary>
    /// A node representing a quest.
    /// </summary>
    public class QuestNode : BaseNode
    {
        /// <summary>
        /// Stores the data related to the quest node.
        /// </summary>
        public QuestNodeData Data { get; protected set; } = null;

        /// <summary>
        /// The field used to edit the quest reference.
        /// </summary>
        private ObjectField _questField = null;

        /// <summary>
        /// Creates a quest node with the given data.
        /// </summary>
        /// <param name="removeGraphViewElement">The method used to remove a <see cref="GraphElement"/> from the <see cref="GraphView"/>.</param>
        /// <param name="styleSheet">The style sheet used for the node.</param>
        /// <param name="position">The position for the node.</param>
        public QuestNode(System.Action<GraphElement> removeGraphViewElement, StyleSheet styleSheet, Vector2 position,
            QuestNodeData data = null)
            : base(removeGraphViewElement, styleSheet, data != null ? data.Position : position)
        {
            Data = data != null ? new QuestNodeData(data) : new QuestNodeData { Guid = Guid };
            title = Data.Quest ? Data.Quest.name : "Quest";
            
            _questField = new ObjectField { value = Data.Quest, objectType = typeof(QuestSO) };
            _questField.RegisterValueChangedCallback((value) =>
            {
                Data.Quest = (QuestSO)value.newValue;
                title = Data.Quest ? Data.Quest.name : "Quest";
            });
            mainContainer.Add(_questField);

            AddPort("Prerequisites", Direction.Input, Port.Capacity.Multi);
            AddPort("Next", Direction.Output, Port.Capacity.Multi);
        }
    }
}
