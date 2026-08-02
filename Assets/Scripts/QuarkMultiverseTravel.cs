using UnityEngine;

public class QuarkMultiverseTravel : MonoBehaviour
{
    public GameObject portal;
    public string targetMultiverse;

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

    public void DeactivatePortal()
    {
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (portal != null && portal.activeSelf && other.CompareTag("Player"))
        {
            TravelToTargetMultiverse();
        }
    }

    public void TravelToTargetMultiverse()
    {
        if (targetMultiverse != null)
        {
            SceneTransition.Instance.LoadScene(targetMultiverse);
        }
    }
}
