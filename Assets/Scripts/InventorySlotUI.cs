using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, ISelectHandler
{
    public Image iconImage;

    private ItemData item;

    public void Setup(ItemData newItem)
    {
        item = newItem;

        if (iconImage != null)
        {
            iconImage.enabled = item != null;
            iconImage.sprite = item != null ? item.icon : null;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (item != null)
        {
            InventoryUI.Instance?.ShowDescription(item);
        }
        else
        {
            InventoryUI.Instance?.ClearDescription();
        }
    }
}
