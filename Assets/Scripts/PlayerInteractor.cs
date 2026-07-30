using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Detection")]
    public float interactRadius = 1.2f;
    public LayerMask interactableLayer;

    [Header("Input")]
    public InputActionReference interactAction;
    
    private Interactable currentInteractable;

    void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
            interactAction.action.performed += OnInteractPerformed;
        }
    }
 
    void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.performed -= OnInteractPerformed;
            interactAction.action.Disable();
        }
    }
 
    void Update()
    {
        Interactable closest = FindClosestInteractable();
 
        if (closest != currentInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.ShowPrompt(false);
            }
 
            if (closest != null)
            {
                closest.ShowPrompt(true);
            }
 
            currentInteractable = closest;
        }
    }
 
    Interactable FindClosestInteractable()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius, interactableLayer);
 
        Interactable closest = null;
        float closestDist = float.MaxValue;
 
        foreach (Collider2D hit in hits)
        {
            Interactable interactable = hit.GetComponent<Interactable>();

            if (interactable == null)
            {
                continue;
            }
 
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            
            if (dist < closestDist)
            {
                closestDist = dist;
                closest = interactable;
            }
        }
 
        return closest;
    }
 
    void OnInteractPerformed(InputAction.CallbackContext ctx)
    {
        currentInteractable?.Interact();
    }
 
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
