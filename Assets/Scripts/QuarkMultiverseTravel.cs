using UnityEngine;

public class QuarkMultiverseTravel : MonoBehaviour
{
    public GameObject portal;

    public void Awake()
    {
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    public void ActivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(true);
        }
    }
}
