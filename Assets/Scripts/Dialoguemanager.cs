using UnityEngine;
using TMPro;
using System.Collections;

public class Dialoguemanager : MonoBehaviour
{
    public static Dialoguemanager Instance { get; private set; }

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public float typingSpeed = 0.04f;

    [Header("Typing Sound")]
    public AudioSource audioSource;
    public AudioClip[] typingSounds;
    public float minSoundInterval = 0.05f;

    public bool IsTyping { get; private set; }
 
    private Coroutine typingCoroutine;
    private string currentFullLine = "";
    private float lastSoundTime = -1f;

    void Awake()
    {
        Instance = this;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void Show(string line)
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(line));
    }

    public void SkipTyping()
    {
        if (!IsTyping)
        {
            return;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        if (dialogueText != null)
        {
            dialogueText.text = currentFullLine;
        }

        IsTyping = false;
    }

    public void Hide()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    IEnumerator TypeText(string line)
    {
        IsTyping = true;
        currentFullLine = line;

        if (dialogueText != null)
        {
            dialogueText.text = "";
        }

        foreach (char c in line)
        {
            if (dialogueText != null)
            {
                dialogueText.text += c;
            }

            PlayTypingSound();
            yield return new WaitForSeconds(typingSpeed);
        }

        IsTyping = false;
    }

    void PlayTypingSound()
    {
        if (audioSource == null || typingSounds == null || typingSounds.Length == 0)
        {
            return;
        }

        if (Time.time - lastSoundTime < minSoundInterval)
        {
            return;
        }

        audioSource.PlayOneShot(typingSounds[Random.Range(0, typingSounds.Length)]);
        lastSoundTime = Time.time;
    }
}
