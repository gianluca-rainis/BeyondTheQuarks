using UnityEngine;

public class QUARKPickupInteraction : MonoBehaviour
{
    public PlayerMovements playerMovements;

    public void PickupQUARK()
    {
        playerMovements.gotQUARK = true;
        Destroy(gameObject);
    }
}
