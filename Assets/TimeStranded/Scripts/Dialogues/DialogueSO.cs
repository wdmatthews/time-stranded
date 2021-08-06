using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Toolkits.NodeEditor;
using Toolkits.Variables;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// Stores data for the dialogue editor.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Time Stranded/Dialogues/Dialogue")]
    public class DialogueSO : BaseEditorDataSO
    {
        /// <summary>
        /// The variables used in this dialogue.
        /// </summary>
        [Tooltip("The variables used in this dialogue.")]
        public List<StringVariableSO> Variables = new List<StringVariableSO>();

        /// <summary>
        /// The list of all message nodes used by the editor.
        /// </summary>
        [Tooltip("The list of all message nodes used by the editor.")]
        public List<MessageNodeData> MessageNodes = new List<MessageNodeData>();

        /// <summary>
        /// The list of all choice nodes used by the editor.
        /// </summary>
        [Tooltip("The list of all choice nodes used by the editor.")]
        public List<ChoiceNodeData> ChoiceNodes = new List<ChoiceNodeData>();

        /// <summary>
        /// The guid of the start node.
        /// </summary>
        [Tooltip("The guid of the start node.")]
        public string StartMessageNodeGuid = "";

        /// <summary>
        /// The message nodes organized by their guid.
        /// </summary>
        [System.NonSerialized] private Dictionary<string, MessageNodeData> _messagesByGuid = null;

        /// <summary>
        /// The choice nodes organized by their guid.
        /// </summary>
        [System.NonSerialized] private Dictionary<string, ChoiceNodeData> _choicesByGuid = null;

        /// <summary>
        /// The variables organized by their name.
        /// </summary>
        [System.NonSerialized] private Dictionary<string, StringVariableSO> _variablesByName = null;

        /// <summary>
        /// Gets a message node by guid.
        /// </summary>
        /// <param name="guid">The message node's guid.</param>
        /// <returns>The message node.</returns>
        public MessageNodeData GetMessageNode(string guid)
        {
            if (_messagesByGuid == null)
            {
                _messagesByGuid = new Dictionary<string, MessageNodeData>();

                for (int i = MessageNodes.Count - 1; i >= 0; i--)
                {
                    MessageNodeData messageNode = MessageNodes[i];
                    _messagesByGuid.Add(messageNode.Guid, messageNode);
                }
            }

            if (_messagesByGuid.ContainsKey(guid))
            {
                MessageNodeData message = _messagesByGuid[guid];
                message.FormattedContent = GetFormattedMessage(message.Content);
                return message;
            }

            return null;
        }

        /// <summary>
        /// Gets a choice node by guid.
        /// </summary>
        /// <param name="guid">The choice node's guid.</param>
        /// <returns>The choice node.</returns>
        public ChoiceNodeData GetChoiceNode(string guid)
        {
            if (_choicesByGuid == null)
            {
                _choicesByGuid = new Dictionary<string, ChoiceNodeData>();

                for (int i = ChoiceNodes.Count - 1; i >= 0; i--)
                {
                    ChoiceNodeData choiceNode = ChoiceNodes[i];
                    _choicesByGuid.Add(choiceNode.Guid, choiceNode);
                }
            }

            return _choicesByGuid.ContainsKey(guid) ? _choicesByGuid[guid] : null;
        }

        /// <summary>
        /// Gets a variable by name.
        /// </summary>
        /// <param name="name">The variable's name.</param>
        /// <returns>The variable.</returns>
        public StringVariableSO GetVariable(string name)
        {
            if (_variablesByName == null)
            {
                _variablesByName = new Dictionary<string, StringVariableSO>();

                for (int i = Variables.Count - 1; i >= 0; i--)
                {
                    StringVariableSO variable = Variables[i];
                    _variablesByName.Add(variable.name, variable);
                }
            }

            return _variablesByName.ContainsKey(name) ? _variablesByName[name] : null;
        }

        /// <summary>
        /// Formats the given message using variables.
        /// </summary>
        /// <param name="message">The message to format.</param>
        /// <returns>The formatted message.</returns>
        private string GetFormattedMessage(string message)
        {
            StringBuilder formattedMessage = new StringBuilder(message);

            for (int i = Variables.Count - 1; i >= 0; i--)
            {
                StringVariableSO variable = Variables[i];
                formattedMessage.Replace($"{{{{{variable.name}}}}}", variable.Value);
            }

            return formattedMessage.ToString();
        }
    }
}
