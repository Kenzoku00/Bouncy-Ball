using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingPopUp;
    public Button openSettingsButton;
    public Button closeButton;
    public float animationDuration = 1f;

    private RectTransform settingPopUpRectTransform;

    void Start()
    {
        settingPopUpRectTransform = settingPopUp.GetComponent<RectTransform>();

        settingPopUp.SetActive(false);
        settingPopUpRectTransform.localScale = Vector3.zero;

        openSettingsButton.onClick.AddListener(OpenSettings);
        closeButton.onClick.AddListener(CloseSettings);
    }

    void OpenSettings()
    {
        settingPopUp.SetActive(true);
        settingPopUpRectTransform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);
    }

    void CloseSettings()
    {
        settingPopUpRectTransform.DOScale(Vector3.zero, animationDuration).SetEase(Ease.InBack)
            .OnComplete(() => settingPopUp.SetActive(false));
    }
}
