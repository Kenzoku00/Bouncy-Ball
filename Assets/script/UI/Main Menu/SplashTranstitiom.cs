using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenControllerWithFade : MonoBehaviour
{
    [SerializeField] private float splashDuration = 3.0f;
    [SerializeField] private string nextSceneName = "MainMenu";
    [SerializeField] private float fadeDuration = 1.0f;
    private CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("CanvasGroup component is missing from the Canvas.");
            return;
        }

        StartCoroutine(SplashSequence());
    }

    private IEnumerator SplashSequence()
    {
        yield return new WaitForSeconds(splashDuration);

        yield return StartCoroutine(FadeOut());

        SceneManager.LoadScene(nextSceneName);
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = canvasGroup.alpha;
        float rate = 1.0f / fadeDuration;

        for (float t = 0; t < 1.0f; t += Time.deltaTime * rate)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0, t);
            yield return null;
        }

        canvasGroup.alpha = 0;
    }
}
