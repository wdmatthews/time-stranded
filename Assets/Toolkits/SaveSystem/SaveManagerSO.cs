using UnityEngine;

namespace Toolkits.SaveSystem
{
    /// <summary>
    /// A base save manager to handle loading and saving files.
    /// </summary>
    /// <typeparam name="T">The save data type.</typeparam>
    public abstract class SaveManagerSO<T> : ScriptableObject
    {
        /// <summary>
        /// The manager's currently used save data for convenience.
        /// </summary>
        [System.NonSerialized] public T SaveData = default;

        /// <summary>
        /// Loads save data into the <c>SaveData</c> field.
        /// </summary>
        /// <param name="saveName">The name used to store the save data.</param>
        /// <returns>The save data.</returns>
        public T Load(string saveName)
        {
            SaveData = JsonUtility.FromJson<T>(LoadFromFile(saveName));
            return SaveData;
        }

        /// <summary>
        /// Returns the save data contents as a string.
        /// By default this string comes from PlayerPrefs.
        /// Override the method to use a different way of loading save data.
        /// </summary>
        /// <param name="saveName">The name used to store the save data.</param>
        /// <returns></returns>
        protected virtual string LoadFromFile(string saveName)
        {
            return PlayerPrefs.GetString(saveName);
        }

        /// <summary>
        /// Saves data.
        /// </summary>
        /// <param name="saveName">The name used to store the save data.</param>
        /// <param name="saveData">The save data. Leave null or empty to use the <c>SaveData</c> field.</param>
        public void Save(string saveName, T saveData = default)
        {
            SaveToFile(saveName, JsonUtility.ToJson(saveData ?? SaveData));
        }

        /// <summary>
        /// Saves the string save data.
        /// By default this string is saved in PlayerPrefs.
        /// Override the method to use a different way of saving data.
        /// </summary>
        /// <param name="saveName">The name used to store the save data.</param>
        /// <param name="saveData">The save data as a string.</param>
        protected virtual void SaveToFile(string saveName, string saveData)
        {
            PlayerPrefs.SetString(saveName, saveData);
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Deletes a save file.
        /// Override the method to use a different way of deleting data.
        /// </summary>
        /// <param name="saveName">The name used to store the save data.</param>
        public virtual void DeleteSave(string saveName)
        {
            PlayerPrefs.DeleteKey(saveName);
            PlayerPrefs.Save();
        }
    }
}
