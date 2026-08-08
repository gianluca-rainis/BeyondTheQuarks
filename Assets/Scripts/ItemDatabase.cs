using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> allItems = new List<ItemData>();

    private Dictionary<string, ItemData> lookup;

    public ItemData GetById(string itemId)
    {
        if (lookup == null)
        {
            BuildLookup();
        }

        lookup.TryGetValue(itemId, out ItemData item);
        
        return item;
    }

    void BuildLookup()
    {
        lookup = new Dictionary<string, ItemData>();

        foreach (ItemData item in allItems)
        {
            if (item != null && !string.IsNullOrEmpty(item.itemId))
            {
                lookup[item.itemId] = item;
            }
        }
    }
}
