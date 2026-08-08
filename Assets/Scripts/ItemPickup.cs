using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Inventory inventory;
    public ItemData item;

    public bool destroyOnPickup = true;

    public void PickUp()
    {
        if (inventory == null || item == null)
        {
            return;
        }

        inventory.AddItem(item);

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
    }
}
