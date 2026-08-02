using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class StageIntroSequence : MonoBehaviour
{
    [Header("References")]
    public PlayerMovements player;
    public PlayerInteractor playerInteractor;

    public GameObject barrier;

    public Transform playerStagePosition;

    [Header("Speech")]
    [TextArea]
    public string[] speechLines;
    public InputActionReference advanceAction;

    [Header("Trigger")]
    public string playerTag = "Player";

    private bool triggered;

    void Awake()
    {
        barrier?.SetActive(false);
    }

    void Reset()
    {
        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered || !other.CompareTag(playerTag))
        {
            return;
        }

        triggered = true;

        if (barrier != null)
        {
            barrier.SetActive(true);
        }

        StartCoroutine(SequenceRoutine());
    }

    IEnumerator SequenceRoutine()
    {
        if (player != null)
        {
            player.enableMovement = false;
        }

        if (playerInteractor != null)
        {
            playerInteractor.enabled = false;
        }

        if (player != null && playerStagePosition != null)
        {
            yield return player.MoveTo(playerStagePosition.position);
        }

        foreach (string line in speechLines)
        {
            Dialoguemanager.Instance?.Show(line);
            yield return WaitForAdvance();
        }

        Dialoguemanager.Instance?.Hide();

        if (player != null)
        {
            player.enableMovement = true;
        }

        if (playerInteractor != null)
        {
            playerInteractor.enabled = true;
        }
    }

    IEnumerator WaitForAdvance()
    {
        yield return null;
 
        while (advanceAction == null || !advanceAction.action.WasPressedThisFrame())
        {
            yield return null;
        }
    }
}
