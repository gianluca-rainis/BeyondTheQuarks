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
 
        Debug.Log(lines[currentLine]);
        currentLine = (currentLine + 1) % lines.Length;
    }
}
