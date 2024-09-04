using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ToggleSliderController : MonoBehaviour
{
    public RectTransform slider;
    public RectTransform upperLimitObject;
    public float speed = 5f;
    public float lowerLimit = -100f;
    public float upperOffset = 10f;

    private bool isMoving = false;
    private bool moveUp = true;

    void Update()
    {
        if (isMoving)
        {
            float upperLimit = upperLimitObject.localPosition.y - upperOffset;

            if (moveUp)
            {
                slider.DOLocalMoveY(upperLimit, Mathf.Abs(upperLimit - slider.localPosition.y) / speed)
                      .SetEase(Ease.InOutCubic)
                      .OnComplete(() =>
                      {
                          isMoving = false;
                          moveUp = false;
                      });
            }
            else
            {
                slider.DOLocalMoveY(lowerLimit, Mathf.Abs(lowerLimit - slider.localPosition.y) / speed)
                      .SetEase(Ease.InOutCubic)
                      .OnComplete(() =>
                      {
                          isMoving = false;
                          moveUp = true;
                      });
            }

            isMoving = false; 
        }
    }

    public void ToggleSlider()
    {
        isMoving = !isMoving;
    }
}
