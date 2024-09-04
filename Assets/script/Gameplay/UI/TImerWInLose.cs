using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TimeWinLose : MonoBehaviour
{
    [SerializeField] private CameraControlAndroid cameraControlAndroid;
    [SerializeField] private Image fillImage;
    [SerializeField] private float fillDuration;
    [SerializeField] private Button returnToMenuButton; 

    private void OnEnable()
    {
        returnToMenuButton.onClick.AddListener(OnReturnToMenuButtonPressed); 
        StartCoroutine(ReduceLoadingBar());
    }

    private void OnDisable()
    {
        returnToMenuButton.onClick.RemoveListener(OnReturnToMenuButtonPressed); 
    }

    private IEnumerator ReduceLoadingBar()
    {
        cameraControlAndroid.freeLookCamera.enabled = false;
        float elapsedTime = 0f;

        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            fillImage.fillAmount = Mathf.Lerp(1, 0, elapsedTime / fillDuration);
            yield return null;
        }

        fillImage.fillAmount = 0;
        SceneManager.LoadScene(0);
    }

    private void OnReturnToMenuButtonPressed()
    {
        SceneManager.LoadScene(0); 
    }
}
