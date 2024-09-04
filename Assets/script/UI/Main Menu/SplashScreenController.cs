using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] private float splashDuration = 3.0f; 
    [SerializeField] private string nextSceneName = "MainMenu";

    private void Start()
    {
        Invoke("LoadNextScene", splashDuration);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
