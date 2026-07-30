using UnityEngine;
using TMPro;

public class Dialoguemanager : MonoBehaviour
{
    public static Dialoguemanager Instance { get; private set; }

    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

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

        if (dialogueText != null)
        {
            dialogueText.text = line;
        }
    }

    public void Hide()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
}
