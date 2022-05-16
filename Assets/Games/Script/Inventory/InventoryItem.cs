using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : SerializedMonoBehaviour
{
    public string inventoryID;
    public string inventoryName;
    [TextArea]
    public string inventoryDescription;
    public InventoryType inventoryType;

    [OnInspectorInit]
    private void OnInspectorInit()
    {
        if (inventoryID == null)
        {
            inventoryID = System.Guid.NewGuid().ToString();
        }
    }

    [Button]
    private void ForceGUIDUpdate()
    {
        inventoryID = System.Guid.NewGuid().ToString();
    }
}

public enum InventoryType
{
    Weapon,
    Armor,
    Accessory
}
