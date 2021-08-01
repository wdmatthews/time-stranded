using Toolkits.NodeEditor;

namespace TimeStranded.Quests
{
    /// <summary>
    /// Stores data about a quest node.
    /// </summary>
    [System.Serializable]
    public class QuestNodeData : BaseNodeData
    {
        /// <summary>
        /// The quest belonging to this node.
        /// </summary>
        public QuestSO Quest = null;

        /// <summary>
        /// A collection of node ids for prerequisite quests.
        /// </summary>
        public string[] PrerequisiteIds = { };

        /// <summary>
        /// A collection of node ids for next quests.
        /// </summary>
        public string[] NextIds = { };

        /// <summary>
        /// Creates empty node data.
        /// </summary>
        public QuestNodeData() { }

        /// <summary>
        /// Creates node data from the given data.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public QuestNodeData(QuestNodeData data)
            : base(data)
        {
            Quest = data.Quest;
            int prerequisiteCount = data.PrerequisiteIds.Length;
            PrerequisiteIds = new string[prerequisiteCount];
            int nextCount = data.NextIds.Length;
            NextIds = new string[nextCount];

            for (int i = 0; i < prerequisiteCount; i++)
            {
                PrerequisiteIds[i] = data.PrerequisiteIds[i];
            }

            for (int i = 0; i < nextCount; i++)
            {
                NextIds[i] = data.NextIds[i];
            }
        }
    }
}
