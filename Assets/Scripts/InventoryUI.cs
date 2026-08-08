using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    [Header("References")]
    public Inventory inventory;
    public InventorySlotUI[] slots;

    [Header("Description Panel")]
    public Image itemPanel;
    public TMP_Text nameText;
    public Image itemImage;
    public TMP_Text descriptionText;

    void Awake()
    {
        Instance = this;
    }

    public void Refresh()
    {
        if (inventory == null || slots == null)
        {
            return;
        }

        for (int i = 0; i < slots.Length; i++)
        {
            ItemData item = i < inventory.items.Count ? inventory.items[i] : null;
            slots[i].Setup(item);
        }
    }

    public void ShowDescription(ItemData item)
    {
        if (itemPanel != null)
        {
            itemPanel.gameObject.SetActive(item != null);
        }

        if (nameText != null)
        {
            nameText.text = item.itemName;
        }

        if (itemImage != null)
        {
            itemImage.sprite = item.icon;
        }

        if (descriptionText != null)
        {
            descriptionText.text = item.description;
        }
    }

    public void ClearDescription()
    {
        if (itemPanel != null)
        {
            itemPanel.gameObject.SetActive(false);
        }

        if (nameText != null)
        {
            nameText.text = "";
        }

        if (itemImage != null)
        {
            itemImage.sprite = null;
        }

        if (descriptionText != null)
        {
            descriptionText.text = "";
        }
    }
}
