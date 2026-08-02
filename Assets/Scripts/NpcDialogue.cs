using UnityEngine;

public class NpcDialogue : MonoBehaviour
{
    [TextArea]
    public string[] lines;

    private int currentLine;

    public void SayLine()
    {
        if (lines == null || lines.Length == 0)
        {
            return;
        }

        if (DialogueManager.Instance != null && DialogueManager.Instance.IsTyping)
        {
            DialogueManager.Instance.SkipTyping();
            return;
        }

        DialogueManager.Instance?.Show(lines[currentLine]);
        currentLine = (currentLine + 1) % lines.Length;
    }
}
