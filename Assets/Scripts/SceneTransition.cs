using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    public Image fadePanel;
    public float fadeDuration = 0.75f;

    void Awake()
    {
        Instance = this;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeAndLoad(sceneName));
    }

    public IEnumerator FadeIn()
    {
        fadePanel.gameObject.SetActive(true);
        yield return StartCoroutine(Fade(1f, 0f));
        fadePanel.raycastTarget = false;
        fadePanel.gameObject.SetActive(false);
    }

    IEnumerator FadeAndLoad(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.raycastTarget = true;
        yield return StartCoroutine(Fade(0f, 1f));
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float elapsed = 0f;
        Color c = fadePanel.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadePanel.color = c;
            yield return null;
        }

        c.a = to;
        fadePanel.color = c;
    }
}