using System;
using System.Linq;
using System.Collections.Generic;
using Unity.FPS.Game;
using Unity.FPS.Gameplay;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Unity.FPS.UI
{
    //jenna's enum
    public enum categories
    {
        Helmet,
        Chest,
        Legs,
        Feets,
        Pistol,
        SMG,
        Shotgun,
        AssaultRifle,
        SniperRifle,
    };

    public enum rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
    };

    public class InGameMenuManager : MonoBehaviour
    {
        [Header("Came with project")]
        [Tooltip("Root GameObject of the menu used to toggle its activation")]
        public GameObject MenuRoot;

        [Tooltip("Master volume when menu is open")] [Range(0.001f, 1f)]
        public float VolumeWhenMenuOpen = 0.5f;

        PlayerInputHandler m_PlayerInputsHandler;
        Health m_PlayerHealth;
        FramerateCounter m_FramerateCounter;


        //------------------------------------\\
        [Header("Jenna's vars below")]
        [Tooltip("The all categories filter")][SerializeField]
        private Toggle _theAllToggle;

        [Tooltip("Horizonal line in the 'all' tickbox that shows when some of the categories are selected")][SerializeField]
        private Image _horizontalLine;

        [Tooltip("Parent to where the items show in menu")][SerializeField]
        private GameObject _itemsContainer;

        [SerializeField]
        private RectTransform _itemsViewportSize;

        [SerializeField]
        private GameObject _selectedItemInfo;

        [Tooltip("List of each category's toggle")][SerializeField]
        private List<Toggle> _toggles;

        private List<InventoryItem> _inventoryItems = new();
        private List<GameObject> _itemsToShow = new();
        private float _itemHeight;
        private RectTransform _itemsContainerSize;
        private ScrollRect _scrollItems;
        private TMP_Text _selectedItemText;
        private int _activeToggleCount; //does NOT include the 'all' toggle
        private List<categories> _activeToggles = new(); 
        private readonly List<categories> _allCategories = Enum.GetValues(typeof(categories)).Cast<categories>().ToList();
        //Jenna's code above

        void Start()
        {
            m_PlayerInputsHandler = FindObjectOfType<PlayerInputHandler>();
            DebugUtility.HandleErrorIfNullFindObject<PlayerInputHandler, InGameMenuManager>(m_PlayerInputsHandler,
                this);

            m_PlayerHealth = m_PlayerInputsHandler.GetComponent<Health>();
            DebugUtility.HandleErrorIfNullGetComponent<Health, InGameMenuManager>(m_PlayerHealth, this, gameObject);

            m_FramerateCounter = FindObjectOfType<FramerateCounter>();
            DebugUtility.HandleErrorIfNullFindObject<FramerateCounter, InGameMenuManager>(m_FramerateCounter, this);

            MenuRoot.SetActive(false);

            //------Jenna's code below------\\
            //setup listeners for each tick box
            _theAllToggle.onValueChanged.AddListener(delegate { OnAllToggleChanged(); });
            foreach (Toggle toggle in _toggles)
            {
                toggle.onValueChanged.AddListener(delegate { OnCategoryToggleChanged(toggle); });
            }

            //seed lists and 
            SetActiveToEachToggle(); 
            foreach(InventoryItem item in _itemsContainer.GetComponentsInChildren<InventoryItem>())
            {
                item.GetComponentInChildren<TMP_Text>().text = item.ItemName;
                _inventoryItems.Add(item);
                item.GetComponent<Button>().onClick.AddListener(delegate { HandleItemClicked(item); });
            }

            //get components of gameObjects now, to save resources later
            _itemHeight = _itemsContainer.GetComponent<GridLayoutGroup>().cellSize.y;
            _itemsContainerSize = _itemsContainer.GetComponent<RectTransform>();
            _scrollItems = _itemsContainer.GetComponent<ScrollRect>();
            _selectedItemText = _selectedItemInfo.GetComponentsInChildren<TMP_Text>()[1]; //2nd child text
        }

        void Update()
        {
            // Lock cursor when clicking outside of menu
            if (!MenuRoot.activeSelf && Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }

            if (Input.GetButtonDown(GameConstants.k_ButtonNamePauseMenu)
                || (MenuRoot.activeSelf && Input.GetButtonDown(GameConstants.k_ButtonNameCancel)))
            {
                SetPauseMenuActivation(!MenuRoot.activeSelf);
            }

            if (Input.GetAxisRaw(GameConstants.k_AxisNameVertical) != 0)
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                    //LookSensitivitySlider.Select();
                }
            }
        }

        public void ClosePauseMenu()
        {
            SetPauseMenuActivation(false);
        }

        void SetPauseMenuActivation(bool active)
        {
            MenuRoot.SetActive(active);

            if (MenuRoot.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Time.timeScale = 0f;
                AudioUtility.SetMasterVolume(VolumeWhenMenuOpen);

                EventSystem.current.SetSelectedGameObject(null);
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1f;
                AudioUtility.SetMasterVolume(1);
            }

        }

        //------Jenna's functions below------\\
        void OnAllToggleChanged()
        {
            foreach (Toggle tog in _toggles)
            {
                //set each category to follow _all tick box
                //use withoutNotify so it doesnt back feed
                tog.SetIsOnWithoutNotify(_theAllToggle.isOn);
            }

            //below replaces CollectActiveToggles(), so we dont have to count every single toggle
            if (_theAllToggle.isOn)
            {
                _activeToggles.Clear();
                SetActiveToEachToggle();
            }
            else
            {
                _activeToggleCount = 0;
                _activeToggles.Clear();
            }
            HandleAnyToggleChanged();
        }

        void OnCategoryToggleChanged(Toggle toggle)
        {
            CollectActiveToggles();
            HandleAnyToggleChanged();
        }

        void HandleAnyToggleChanged()
        {
            ChangeAllToggleImage();
            ChangeShowList();
            ShowItems();
        }

        void ChangeAllToggleImage()
        {
            //int _activeToggleCount = CurrentActiveCategoryToggles().Count;
            if (_activeToggleCount == 0) //none active, show empty
            {
                _horizontalLine.enabled = false;
                _theAllToggle.SetIsOnWithoutNotify(false);
            }
            else if (_activeToggleCount == _toggles.Count) //all active, show tick
            {
                _horizontalLine.enabled = false;
                _theAllToggle.SetIsOnWithoutNotify(true);
            }
            else if (_activeToggleCount > 0 && _activeToggleCount < _toggles.Count) //some active, show line
            {
                _horizontalLine.enabled = true;
                _theAllToggle.SetIsOnWithoutNotify(false);
            }
            else
            {
                 throw new Exception("Active toggles count is " + _activeToggleCount + ", which is not between 0 and " + _toggles.Count);
            }
        }

        void CollectActiveToggles()
        {
            _activeToggleCount = 0;
            _activeToggles.Clear();

            foreach (Toggle tog in _toggles)
            {
                if (tog.isOn)
                {
                    _activeToggleCount++;
                    _activeToggles.Add(tog.GetComponentInParent<InventoryFilter>().Category);
                }
            }
        }

        void ChangeShowList()
        {
            _itemsToShow.Clear();
            foreach (InventoryItem item in _inventoryItems)
            {
                if (_activeToggles.Contains(item.Category))
                {
                    _itemsToShow.Add(item.gameObject);
                }
            }
        }

        void ShowItems()
        {
            foreach (InventoryItem item in _inventoryItems) //hide all
            {
                item.gameObject.SetActive(false);
            }
            foreach (GameObject item in _itemsToShow) //show only the correct filtered items
            {
                item.SetActive(true);
            }

            int countItemsCanSee = Mathf.FloorToInt(_itemsViewportSize.rect.height / _itemHeight);

            if (_itemsToShow.Count > countItemsCanSee) //need to scroll
            {
                if (_scrollItems.movementType != ScrollRect.MovementType.Elastic)
                {
                    _scrollItems.movementType = ScrollRect.MovementType.Elastic;
                }
                _itemsContainerSize.sizeDelta = 
                    new Vector2(_itemsContainerSize.sizeDelta.x, _itemHeight * (_itemsToShow.Count + 1)); //+1 for heading
            }
            else //no need for scroll
            {
                if (_scrollItems.movementType != ScrollRect.MovementType.Clamped)
                {
                    _scrollItems.movementType = ScrollRect.MovementType.Clamped;
                }
            }
        }

        void SetActiveToEachToggle()
        {
            foreach (categories cat in _allCategories)
            {
                _activeToggleCount = _toggles.Count;
                _activeToggles.Add(cat);
            }
        }

        void ChangeSelectedInfo(InventoryItem item)
        {
            string text =
                "Name =\n  " + item.ItemName +
                "\nWeight =\n  " + item.Weight +
                "\nRarity =\n  " + item.Rarity;

            if (item.Damage > 0) text += "\nDamage =\n  " + item.Damage;
            if (item.Armour > 0) text += "\nArmour =\n  " + item.Armour;

            _selectedItemText.text = text;
        }

        void HandleItemClicked(InventoryItem item)
        {
            ChangeSelectedInfo(item);
        }
    }
}