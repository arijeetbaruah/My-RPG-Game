using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

namespace RPG.UI
{
    public struct InventoryPicker
    {
        [InlineProperty, HideLabel, ValueDropdown("DropdownElement")]
        public InventoryItem inventoryItem;

        private static IEnumerable DropdownElement()
        {
            return InventoryConfig.Instance.inventoryRegistory;
        }
    }

    public class InventorySystem : SerializedMonoBehaviour
    {
        public MenuInputReader menuinputReader;
        public Input.InputReader inputReader;

        [SerializeField] private InventoryElement inventoryElement;
        [SerializeField] private Transform content;

        [ShowInInspector, SerializeField]
        private List<InventoryPicker> inventoryItems = new List<InventoryPicker>();

        private InventoryItem weaponEquipedItem;
        private InventoryItem armorEquipedItem;
        private InventoryItem accessoryEquipedItem;

        private Dictionary<InventoryItem, InventoryElement> buttonList = new Dictionary<InventoryItem, InventoryElement>();
        private InventoryItem currentButton;

        public List<InventoryPicker> InventoryItemSorted 
        {
            get
            {
                if (useFilter)
                {
                    return inventoryItems.Where(item => item.inventoryItem.inventoryType == filter).ToList();
                }

                return inventoryItems.OrderBy(item => item.inventoryItem.inventoryType).ToList();
            }
        }
        
        public InventoryType filter;
        public bool useFilter = false;

        public List<InventoryItem> equipedItem
        {
            get
            {
                return new List<InventoryItem>() {
                    weaponEquipedItem,
                    armorEquipedItem,
                    accessoryEquipedItem
                };
            }
        }

        private void Start()
        {
            inputReader.InventoryEvent += OpenAndCloseInventory;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            inputReader.InventoryEvent -= OpenAndCloseInventory;
        }

        public void OpenAndCloseInventory()
        {
            gameObject.SetActive(!gameObject.activeSelf);

            menuinputReader.SetMenuInput(gameObject.activeSelf);
            inputReader.SetInputActive(gameObject.activeSelf);
            if (gameObject.activeSelf)
            {
                currentButton = InventoryItemSorted[0].inventoryItem;
                SpawnElements();
            }
        }

        public void SpawnElements()
        {
            buttonList.Clear();
            foreach (Transform element in content)
            {
                Destroy(element.gameObject);
            }

            foreach (InventoryPicker inventoryItem in InventoryItemSorted)
            {
                var button = Instantiate(inventoryElement, content);
                button.SetInventoryElement(inventoryItem.inventoryItem);
                button.onClickEvent += EquipItem;
                buttonList.Add(inventoryItem.inventoryItem, button);
                
                button.SetEquiped(equipedItem.Contains(inventoryItem.inventoryItem));
            }
            EventSystem.current.SetSelectedGameObject(buttonList[currentButton].gameObject);
        }

        public void FilterAll()
        {
            useFilter = false;

            currentButton = InventoryItemSorted[0].inventoryItem;
            SpawnElements();
        }

        public void FilterWeapon()
        {
            useFilter = true;
            filter = InventoryType.Weapon;

            currentButton = InventoryItemSorted[0].inventoryItem;
            SpawnElements();
        }

        public void FilterArmor()
        {
            useFilter = true;
            filter = InventoryType.Armor;

            currentButton = InventoryItemSorted[0].inventoryItem;
            SpawnElements();
        }

        public void FilterAccessory()
        {
            useFilter = true;
            filter = InventoryType.Accessory;

            currentButton = InventoryItemSorted[0].inventoryItem;
            SpawnElements();
        }

        public void EquipItem(InventoryItem newEquipedItem)
        {
            if (newEquipedItem != null && newEquipedItem.inventoryType == InventoryType.Weapon)
            {
                weaponEquipedItem = newEquipedItem;
            }
            else if (newEquipedItem != null && newEquipedItem.inventoryType == InventoryType.Armor)
            {
                armorEquipedItem = newEquipedItem;
            }
            else if (newEquipedItem != null && newEquipedItem.inventoryType == InventoryType.Accessory)
            {
                accessoryEquipedItem = newEquipedItem;
            }
            currentButton = newEquipedItem;

            SpawnElements();
        }
    }
}
