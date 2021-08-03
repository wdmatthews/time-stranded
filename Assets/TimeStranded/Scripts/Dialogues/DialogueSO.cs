using System.Collections.Generic;
using UnityEngine;
using Toolkits.NodeEditor;

namespace TimeStranded.Dialogues
{
    /// <summary>
    /// Stores data for the dialogue editor.
    /// </summary>
    [CreateAssetMenu(fileName = "NewDialogue", menuName = "Time Stranded/Dialogues/Dialogue")]
    public class DialogueSO : BaseEditorDataSO
    {
        /// <summary>
        /// The list of all message nodes used by the editor.
        /// </summary>
        public List<MessageNodeData> MessageNodes = new List<MessageNodeData>();

        /// <summary>
        /// The list of all choice nodes used by the editor.
        /// </summary>
        public List<ChoiceNodeData> ChoiceNodes = new List<ChoiceNodeData>();

        /// <summary>
        /// The guid of the start node.
        /// </summary>
        public string StartMessageNodeGuid = "";

        /// <summary>
        /// The message nodes organized by their guid.
        /// </summary>
        private Dictionary<string, MessageNodeData> _messagesByGuid = null;

        /// <summary>
        /// The choice nodes organized by their guid.
        /// </summary>
        private Dictionary<string, ChoiceNodeData> _choicesByGuid = null;

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

            return _messagesByGuid.ContainsKey(guid) ? _messagesByGuid[guid] : null;
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
    }
}
