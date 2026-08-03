using UnityEngine;
using UnityEvents = UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Interactable : MonoBehaviour
{
    public UnityEvents.UnityEvent onInteract;
    public GameObject promptIcon;
    public bool oneTimeUse = false;

    void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();
        
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void Awake()
    {
        ShowPrompt(false);
    }

    public void ShowPrompt(bool show)
    {
        if (promptIcon != null)
        {
            promptIcon.SetActive(show);
        }
    }

    public void Interact()
    {
        if (oneTimeUse)
        {
            enabled = false;
            gameObject.SetActive(false);
            promptIcon?.SetActive(false);
        }

        onInteract?.Invoke();
    }
}
