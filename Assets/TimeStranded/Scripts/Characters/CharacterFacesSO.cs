using UnityEngine;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Stores all of the faces to use, for easy access by scripts.
    /// </summary>
    [CreateAssetMenu(fileName = "Faces", menuName = "Time Stranded/Characters/Faces")]
    public class CharacterFacesSO : DictionarySO<CharacterFaceSO> { }
}
