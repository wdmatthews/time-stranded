using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Toolkits.NodeEditor.Editor;
using TimeStranded.Characters;

namespace TimeStranded.Dialogues.Editor
{
    /// <summary>
    /// A node representing a message.
    /// </summary>
    public class MessageNode : BaseNode
    {
        /// <summary>
        /// Stores the data related to the message node.
        /// </summary>
        public MessageNodeData Data { get; protected set; } = null;

        /// <summary>
        /// The field used to edit the speaker reference.
        /// </summary>
        private ObjectField _speakerField = null;

        /// <summary>
        /// The field used to edit the message content.
        /// </summary>
        private TextField _contentField = null;

        /// <summary>
        /// Creates a message node with the given data.
        /// </summary>
        /// <param name="removeGraphViewElement">The method used to remove a <see cref="GraphElement"/> from the <see cref="GraphView"/>.</param>
        /// <param name="styleSheet">The style sheet used for the node.</param>
        /// <param name="position">The position for the node.</param>
        public MessageNode(System.Action<GraphElement> removeGraphViewElement, StyleSheet styleSheet, Vector2 position,
            MessageNodeData data = null)
            : base(removeGraphViewElement, styleSheet, data != null ? data.Position : position)
        {
            Data = data != null ? new MessageNodeData(data) : new MessageNodeData { Guid = Guid };
            title = "Message";

            _speakerField = new ObjectField { value = Data.Speaker, objectType = typeof(CharacterSO) };
            _speakerField.RegisterValueChangedCallback((value) =>
            {
                Data.Speaker = (CharacterSO)value.newValue;
            });
            mainContainer.Add(_speakerField);

            _contentField = new TextField { value = Data.Content };
            _contentField.RegisterValueChangedCallback((value) =>
            {
                Data.Content = value.newValue;
            });
            mainContainer.Add(_contentField);

            AddPort("Previous", Direction.Input, Port.Capacity.Multi);
            AddPort("Next", Direction.Output, Port.Capacity.Single);
        }
    }
}
