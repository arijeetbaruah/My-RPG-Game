using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Inventory Config")]
public class InventoryConfig : SerializedScriptableObject
{
    public static InventoryConfig instance = null;

    public static InventoryConfig Instance {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<InventoryConfig>("Inventory Config");
            }

            return instance;
        }
    }

    public Dictionary<InventoryType, PreviewableSprite> icons;
    public List<InventoryItem> inventoryRegistory;

    public struct PreviewableSprite
    {
        [PreviewField, HideLabel, InlineProperty]
        public Sprite sprite;
    }
}
