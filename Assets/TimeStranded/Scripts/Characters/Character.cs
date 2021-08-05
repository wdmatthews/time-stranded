using System.Collections.Generic;
using UnityEngine;
using TimeStranded.Attributes;
using TimeStranded.Inventory;

namespace TimeStranded.Characters
{
    /// <summary>
    /// Provides controls for a character.
    /// </summary>
    [AddComponentMenu("Time Stranded/Characters/Character")]
    [DisallowMultipleComponent]
    public class Character : MonoBehaviour
    {
        /// <summary>
        /// The character's data.
        /// </summary>
        [Tooltip("The character's data.")]
        public CharacterSO Data = null;

        /// <summary>
        /// The collection of character faces.
        /// </summary>
        [Tooltip("The collection of character faces.")]
        [SerializeField] protected CharacterFacesSO _faces = null;

        /// <summary>
        /// The collection of character colors.
        /// </summary>
        [Tooltip("The collection of character colors.")]
        [SerializeField] protected CharacterColorsSO _colors = null;

        /// <summary>
        /// The character's rigibody.
        /// </summary>
        [Tooltip("The character's rigibody.")]
        public Rigidbody2D Rigidbody = null;

        /// <summary>
        /// The character's face.
        /// </summary>
        [Tooltip("The character's face.")]
        [SerializeField] protected SpriteRenderer _face = null;

        /// <summary>
        /// The character's fill.
        /// </summary>
        [Tooltip("The character's fill.")]
        [SerializeField] protected SpriteRenderer _fill = null;

        /// <summary>
        /// A transform used to contain any items the character is holding.
        /// </summary>
        [Tooltip("A transform used to contain any items the character is holding.")]
        [SerializeField] protected Transform _itemHolder = null;

        /// <summary>
        /// The name of the movement speed attribute.
        /// </summary>
        [Tooltip("The name of the movement speed attribute.")]
        [SerializeField] protected string _speedAttributeName = "Speed";

        /// <summary>
        /// The name of the health attribute.
        /// </summary>
        [Tooltip("The name of the health attribute.")]
        [SerializeField] protected string _healthAttributeName = "Health";

        /// <summary>
        /// The event channel to raise when a character is healed.
        /// </summary>
        [Tooltip("The event channel to raise when a character is healed.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterHeal = null;

        /// <summary>
        /// The event channel to raise when a character is damaged.
        /// </summary>
        [Tooltip("The event channel to raise when a character is damaged.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterDamage = null;

        /// <summary>
        /// The event channel to raise when a character dies.
        /// </summary>
        [Tooltip("The event channel to raise when a character dies.")]
        [SerializeField] protected CharacterEventChannelSO _onCharacterDeath = null;

        /// <summary>
        /// The current aim direction.
        /// </summary>
        protected Vector2 _aimDirection = new Vector2();

        /// <summary>
        /// The item currently being held.
        /// </summary>
        protected Item _activeItem = null;

        /// <summary>
        /// The runtime instances of the character's attributes.
        /// </summary>
        protected AttributeSO[] _attributes = { };

        /// <summary>
        /// The runtime instances of the character's attributes organized by name.
        /// </summary>
        [System.NonSerialized] public Dictionary<string, AttributeSO> AttributesByName = new Dictionary<string, AttributeSO>();

        /// <summary>
        /// The movement speed attribute.
        /// </summary>
        protected AttributeSO _speedAttribute = null;

        /// <summary>
        /// The health attribute.
        /// </summary>
        protected AttributeSO _healthAttribute = null;

        /// <summary>
        /// Whether or not the character is dead.
        /// </summary>
        protected bool _isDead = false;

        /// <summary>
        /// Stores all of the abilities for the character to use.
        /// </summary>
        protected List<ItemStack> _abilities = new List<ItemStack>();

        /// <summary>
        /// The currently selected ability.
        /// </summary>
        [System.NonSerialized] public ItemStack SelectedAbility = null;

        /// <summary>
        /// The team that the player is on.
        /// Only used during a match.
        /// </summary>
        [System.NonSerialized] public string Team = "";

        /// <summary>
        /// The number of respawns left.
        /// Only used during a match.
        /// </summary>
        [System.NonSerialized] public int RespawnsLeft = 0;

        /// <summary>
        /// The time until respawn.
        /// Only used during a match.
        /// </summary>
        [System.NonSerialized] public float TimeUntilRespawn = 0;

        /// <summary>
        /// Whether or not the character is respawning.
        /// Only used during a match.
        /// </summary>
        [System.NonSerialized] public bool IsRespawning = false;

        /// <summary>
        /// The method to respawn the character.
        /// Only used during a match.
        /// </summary>
        public System.Action<Character> Respawn = null;

        private void Awake()
        {
            if (Data) Initialize(Data);
        }

        private void Update()
        {
            // Update the attribute's modifier timers.
            for (int i = _attributes.Length - 1; i >= 0; i--)
            {
                _attributes[i].OnUpdate();
            }
        }

        /// <summary>
        /// Updates characters during a match.
        /// </summary>
        public void OnUpdateInMatch()
        {
            if (_isDead && IsRespawning)
            {
                if (Mathf.Approximately(TimeUntilRespawn, 0)) OnRespawn();
                else TimeUntilRespawn = Mathf.Clamp(TimeUntilRespawn - Time.deltaTime, 0, TimeUntilRespawn);
            }
        }

        /// <summary>
        /// Initializes the character with the given data.
        /// </summary>
        /// <param name="data">The character's data.</param>
        public void Initialize(CharacterSO data)
        {
            Data = data;
            // Initialize the character's face and color.
            SetFace(Data.Face);
            SetColor(Data.Color);
            // Initialize the character's attributes.
            int attributeCount = Data.Attributes.Length;
            _attributes = new AttributeSO[attributeCount];
            AttributesByName = new Dictionary<string, AttributeSO>();

            for (int i = attributeCount - 1; i >= 0; i--)
            {
                AttributeSO attribute = Data.Attributes[i].Copy();
                _attributes[i] = attribute;
                AttributesByName.Add(attribute.Name, attribute);
            }

            _speedAttribute = AttributesByName[_speedAttributeName];
            _speedAttribute.Value = _speedAttribute.MinValue;
            _healthAttribute = AttributesByName[_healthAttributeName];
            _healthAttribute.Value = _healthAttribute.MaxValue;
        }

        /// <summary>
        /// Starts moving in the given direction.
        /// </summary>
        /// <param name="direction">The direction to move in.</param>
        public void Move(Vector2 direction)
        {
            Rigidbody.velocity = _speedAttribute.Value * direction;
        }

        /// <summary>
        /// Aims in the given direction.
        /// </summary>
        /// <param name="direction">The direction to aim in.</param>
        public void Aim(Vector2 direction)
        {
            _aimDirection = direction;
            float angle = Mathf.Rad2Deg * Mathf.Atan2(_aimDirection.y, _aimDirection.x);
            transform.eulerAngles = new Vector3(0, 0, angle);
            _face.transform.localEulerAngles = new Vector3(0, 0, -angle);
        }

        /// <summary>
        /// Sets the character's face.
        /// </summary>
        /// <param name="faceName">The name of the face.</param>
        public void SetFace(string faceName)
        {
            _face.sprite = _faces[faceName].Sprite;
        }

        /// <summary>
        /// Sets the character's face.
        /// </summary>
        /// <param name="face">The face.</param>
        public void SetFace(CharacterFaceSO face)
        {
            _face.sprite = face.Sprite;
        }

        /// <summary>
        /// Sets the character's color.
        /// </summary>
        /// <param name="colorName">The name of the color.</param>
        public void SetColor(string colorName)
        {
            _fill.color = _colors[colorName].Color;
        }

        /// <summary>
        /// Sets the character's color.
        /// </summary>
        /// <param name="color">The color.</param>
        public void SetColor(CharacterColorSO color)
        {
            _fill.color = color.Color;
        }

        /// <summary>
        /// Uses the item the character is holding.
        /// </summary>
        public void UseItem()
        {
            if (!_activeItem) return;
            _activeItem.Use(this);
            // Detect if the item was a one time use, and remove one of its uses.
            if (_activeItem.ItemData.IsOneTimeUse) Data.Inventory.RemoveItem(_activeItem.ItemData, 1);
            // Detect if the item was an ability, and remove it if necessary.
            if (SelectedAbility != null && SelectedAbility.Amount == 0) RemoveAbility(SelectedAbility);
        }

        /// <summary>
        /// Makes the character hold an item.
        /// </summary>
        /// <param name="item">The item to hold.</param>
        public void HoldItem(Item item)
        {
            if (_activeItem && !_activeItem.ItemData.CanBeSelected) return;
            _activeItem = item;
            _activeItem.transform.parent = _itemHolder;
            _activeItem.transform.localPosition = new Vector3();
            _activeItem.transform.localEulerAngles = new Vector3();
            _activeItem.transform.localScale = new Vector3(1, 1, 1);
            _activeItem.OnPickup(this);
        }

        /// <summary>
        /// Makes the character release their hold on the item they were holding.
        /// </summary>
        public void ReleaseItem()
        {
            _activeItem.OnRelease(this);
            _activeItem.transform.parent = null;
            _activeItem = null;
        }

        /// <summary>
        /// Adds an ability to the character.
        /// </summary>
        /// <param name="ability">The ability to add.</param>
        public void AddAbility(ItemStack ability)
        {
            _abilities.Add(ability);
        }

        /// <summary>
        /// Removes an ability from the character.
        /// </summary>
        /// <param name="ability">The ability to remove.</param>
        public void RemoveAbility(ItemStack ability)
        {
            _abilities.Remove(ability);
            // Stop holding the ability if removing a selected ability.
            if (_activeItem && _activeItem.ItemData == ability.Item)
            {
                _activeItem.gameObject.SetActive(false);
                ReleaseItem();
            }
        }

        /// <summary>
        /// Selects an ability if able to.
        /// </summary>
        /// <param name="abilityIndex">The ability's index.</param>
        public void SelectAbility(int abilityIndex)
        {
            // Stop if not able to select an ability.
            if (_activeItem && !_activeItem.ItemData.CanBeSelected) return;

            // Select the ability.
            ItemStack abilityStack = _abilities[abilityIndex];
            ItemSO abilityData = abilityStack.Item;
            // The ability is not changing, so do not do anything.
            if (_activeItem && _activeItem.ItemData == abilityData) return;
            SelectedAbility = abilityStack;
            // Release the old item if there is one.
            if (_activeItem) ReleaseItem();
            // Hold the selected ability after requesting an instance.
            HoldItem(abilityData.Request());
        }

        /// <summary>
        /// Heals the character.
        /// </summary>
        /// <param name="amount">How much health to regain.</param>
        public void Heal(float amount)
        {
            _healthAttribute.ChangeValue(amount);
            _onCharacterHeal.Raise(this);
        }

        /// <summary>
        /// Damages the character.
        /// </summary>
        /// <param name="amount">How much damage to take.</param>
        public void TakeDamage(float amount)
        {
            _healthAttribute.ChangeValue(-amount);

            if (Mathf.Approximately(_healthAttribute.Value, 0))
            {
                // Remove any attribute modifiers.
                for (int i = _attributes.Length - 1; i >= 0; i--)
                {
                    _attributes[i].RemoveAllModifiers();
                }

                // Mark the character as dead.
                _isDead = true;
                _onCharacterDeath.Raise(this);
                gameObject.SetActive(false);
            }
            else _onCharacterDamage.Raise(this);
        }

        /// <summary>
        /// Called when respawning.
        /// </summary>
        protected void OnRespawn()
        {
            _isDead = false;
            Respawn(this);
            gameObject.SetActive(true);
            _healthAttribute.Value = _healthAttribute.MaxValue;
        }
    }
}
