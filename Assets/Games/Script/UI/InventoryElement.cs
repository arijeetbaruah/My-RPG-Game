using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryElement : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI elementName;
    [SerializeField] private Color elementEquipColor;
    [SerializeField] private Image elementIcon;
    [SerializeField] private Image bgImage;

    public System.Action<InventoryItem> onClickEvent;
    public InventoryItem item;
    private bool equiped = false;

    public void SetInventoryElement(InventoryItem inventoryItem)
    {
        item = inventoryItem;
        elementIcon.sprite = InventoryConfig.Instance.icons[inventoryItem.inventoryType].sprite;

        elementName.SetText(inventoryItem.inventoryName);
    }

    public void SetEquiped(bool equiped)
    {
        this.equiped = equiped;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (equiped)
        {
            bgImage.color = elementEquipColor;
        }
        else
        {
            bgImage.color = Color.white;
        }
    }

    public void OnClick()
    {
        onClickEvent?.Invoke(item);
    }
}
