using UnityEngine;

public class Resume : MonoBehaviour
{
    public void ResumeGame()
    {
        if (!SaveSystem.SaveExists())
        {
            return;
        }

        SaveData data = SaveSystem.GetSavedData();

        if (data == null)
        {
            return;
        }

        LoadManager.loadedData = data;

        SceneTransition.Instance.LoadScene(data.sceneName);
    }
}
