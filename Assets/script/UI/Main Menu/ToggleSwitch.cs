using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ToggleSwitch : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    public Button toggleButton;
    public RectTransform switchTransform;
    public Color onColor = Color.green;
    public Color offColor = Color.red;
    private bool isOn = false;
    private Vector2 onPosition;
    private Vector2 offPosition;
    public float animationDuration = 0.5f;

    void Start()
    {
        toggleButton.onClick.AddListener(OnButtonClick);
        onPosition = new Vector2(47, switchTransform.anchoredPosition.y); 
        offPosition = new Vector2(-47, switchTransform.anchoredPosition.y); 
        UpdateSwitch();
    }

    void OnButtonClick()
    {
        Toggle();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Ndak Jadi
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateSwitch();
    }

    void UpdateSwitch()
    {
        Vector2 targetPosition = isOn ? onPosition : offPosition;
        Color targetColor = isOn ? onColor : offColor;

        switchTransform.DOKill();
        switchTransform.GetComponent<Image>().DOKill();

        switchTransform.DOAnchorPos(targetPosition, animationDuration).SetEase(Ease.InOutCubic);

        switchTransform.GetComponent<Image>().DOColor(targetColor, animationDuration);
    }
}
