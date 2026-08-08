using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public ItemDatabase database;
    public List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if (item != null)
        {
            items.Add(item);
        }
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
    }

    public void SaveInventory(SaveData data)
    {
        data.inventoryItems = new List<string>();

        foreach (ItemData item in items)
        {
            if (item != null)
            {
                data.inventoryItems.Add(item.itemId);
            }
        }
    }

    public void LoadInventory(SaveData data)
    {
        items = new List<ItemData>();

        if (database == null)
        {
            return;
        }

        foreach (string id in data.inventoryItems)
        {
            ItemData item = database.GetById(id);

            if (item != null)
            {
                items.Add(item);
            }
        }
    }
}
