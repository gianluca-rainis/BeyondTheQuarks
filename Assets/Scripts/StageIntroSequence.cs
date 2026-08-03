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

    [Header("QUARK Pickup")]
    public QUARKPickupInteraction quark;
    public GameObject portalOpener;

    [Header("Return")]
    public Transform playerReturnPosition;

    [Header("Pedestal")]
    public Transform pedestal;
    public Transform pedestalTargetPosition;
    public float pedestalMoveDuration = 1.5f;

    [Header("Speech")]
    [TextArea]
    public string[] speechLines;
    public InputActionReference advanceAction;

    [Header("Sounds")]
    public AudioSource audioSource;

    [Header("Trigger")]
    public string playerTag = "Player";

    private bool triggered;

    void Awake()
    {
        portalOpener?.SetActive(false);
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

        audioSource?.Stop();

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

            player.SetFacing(FacingDirection.Down);
        }

        if (advanceAction != null)
        {
            advanceAction.action.Enable();
        }

        foreach (string line in speechLines)
        {
            DialogueManager.Instance?.Show(line);
            yield return WaitForAdvance();
        }

        DialogueManager.Instance?.Hide();

        player.SetFacing(FacingDirection.Up);

        if (playerInteractor != null)
        {
            playerInteractor.enabled = true;
        }

        if (quark != null)
        {
            yield return new WaitUntil(() => quark == null);
        }

        if (playerInteractor != null)
        {
            playerInteractor.enabled = false;
        }

        if (pedestal != null && pedestalTargetPosition != null)
        {
            yield return MovePedestal();
        }

        if (player != null && playerReturnPosition != null)
        {
            yield return player.MoveTo(playerReturnPosition.position);

            player.SetFacing(FacingDirection.Up);
        }

        portalOpener?.SetActive(true);

        if (playerInteractor != null)
        {
            playerInteractor.enabled = true;
        }

        if (player != null)
        {
            player.enableMovement = true;
        }
    }

    IEnumerator MovePedestal()
    {
        Vector3 startPos = pedestal.position;
        Vector3 endPos = pedestalTargetPosition.position;
        float elapsed = 0f;

        while (elapsed < pedestalMoveDuration)
        {
            elapsed += Time.deltaTime;
            pedestal.position = Vector3.Lerp(startPos, endPos, elapsed / pedestalMoveDuration);

            yield return null;
        }

        pedestal.position = endPos;
    }

    IEnumerator WaitForAdvance()
    {
        yield return null;

        while (true)
        {
            yield return null;

            if (advanceAction == null || !advanceAction.action.WasPressedThisFrame())
            {
                continue;
            }

            if (DialogueManager.Instance != null && DialogueManager.Instance.IsTyping)
            {
                DialogueManager.Instance.SkipTyping();
                continue;
            }

            break;
        }
    }
}
