using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemId;

    public string itemName;

    [TextArea(2, 5)]
    public string description;

    public Sprite icon;
}
