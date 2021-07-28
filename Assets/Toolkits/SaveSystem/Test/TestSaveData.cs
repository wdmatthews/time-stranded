using UnityEngine;

namespace Toolkits.SaveSystem.Test
{
    /// <summary>
    /// A test use of save data.
    /// </summary>
    [System.Serializable]
    public class TestSaveData
    {
        /// <summary>
        /// String data to save.
        /// </summary>
        public string StringData = "";

        /// <summary>
        /// Integer data to save.
        /// </summary>
        public int IntData = 0;

        /// <summary>
        /// Vector2 data to save.
        /// </summary>
        public Vector2 Vector2Data = new Vector2();
    }
}
