using System.Collections.Generic;
using UnityEngine;
using Toolkits.NodeEditor;

namespace TimeStranded.Quests
{
    /// <summary>
    /// Stores data for the quest node editor.
    /// </summary>
    [CreateAssetMenu(fileName = "NewQuestEditorData", menuName = "Time Stranded/Quests/Quest Editor Data")]
    public class QuestEditorDataSO : BaseEditorDataSO
    {
        /// <summary>
        /// The list of all quest nodes used by the editor.
        /// </summary>
        public List<QuestNodeData> QuestNodes = new List<QuestNodeData>();

        /// <summary>
        /// The quests organized by their guid.
        /// </summary>
        private Dictionary<string, QuestSO> _questsByGuid = null;

        /// <summary>
        /// Gets a quest by guid.
        /// </summary>
        /// <param name="guid">The quest's guid.</param>
        /// <returns>The quest.</returns>
        public QuestSO this[string guid]
        {
            get
            {
                if (_questsByGuid == null)
                {
                    _questsByGuid = new Dictionary<string, QuestSO>();

                    for (int i = QuestNodes.Count - 1; i >= 0; i--)
                    {
                        QuestNodeData questData = QuestNodes[i];
                        _questsByGuid.Add(questData.Guid, questData.Quest);
                    }
                }

                return _questsByGuid[name];
            }
        }
    }
}
