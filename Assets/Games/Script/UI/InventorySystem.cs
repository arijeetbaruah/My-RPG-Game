using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Input;
using Sirenix.OdinInspector;

namespace RPG.UI
{
    public class InventorySystem : SerializedMonoBehaviour
    {
        public MenuInputReader inputReader;

        private void Start()
        {
            inputReader.OnInventoryToggle += OpenAndCloseInventory;

            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            inputReader.OnInventoryToggle -= OpenAndCloseInventory;
        }

        public void OpenAndCloseInventory()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
