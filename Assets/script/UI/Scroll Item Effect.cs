using UnityEngine;
using UnityEngine.UI;

public class ScrollItemEffect : MonoBehaviour
{
    public float fadeDistance = 200f; // Sesuaikan sesuai kebutuhan
    public float fadeSpeed = 5f; // Kecepatan interpolasi fade

    private RectTransform contentRectTransform;
    private ScrollRect scrollRect;

    void Start()
    {
        contentRectTransform = GetComponent<RectTransform>();
        scrollRect = GetComponentInParent<ScrollRect>();
    }

    void Update()
    {
        foreach (Transform child in contentRectTransform)
        {
            RectTransform rectTransform = child.GetComponent<RectTransform>();
            CanvasGroup canvasGroup = child.GetComponent<CanvasGroup>();

            Vector3[] corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            float distance = Mathf.Max(Mathf.Abs(corners[0].y - scrollRect.transform.position.y), Mathf.Abs(corners[2].y - scrollRect.transform.position.y));

            float targetAlpha = Mathf.Clamp01(1 - distance / fadeDistance);
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);
        }
    }
}
