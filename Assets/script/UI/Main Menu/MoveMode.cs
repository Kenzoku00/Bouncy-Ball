using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveMode : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public Button button;
    public float moveDuration = 2.0f;
    public Ease easeType = Ease.InOutCubic;

    private Vector3 targetPosition1;
    private Vector3 targetPosition2;
    private Vector3 secondTargetPosition1;
    private Vector3 secondTargetPosition2;
    private Vector3 moveAfterTeleportPositionObject1;
    private Vector3 moveAfterTeleportPositionObject2;

    private int clickCount = 0;

    void Start()
    {

        targetPosition1 = new Vector3(0, 150f, 0);
        targetPosition2 = new Vector3(0, 0f, 0);
        secondTargetPosition1 = new Vector3(0, -150f, 0);
        secondTargetPosition2 = new Vector3(0, 150f, 0);
        moveAfterTeleportPositionObject1 = new Vector3(0, 0f, 0);
        moveAfterTeleportPositionObject2 = new Vector3(0, 0f, 0);


        Debug.Log("Initial object1 position: " + object1.GetComponent<RectTransform>().anchoredPosition);
        Debug.Log("Initial object2 position: " + object2.GetComponent<RectTransform>().anchoredPosition);
    }

    void OnButtonPress()
    {
        if (clickCount == 0)
        {
            MoveObjectToPosition(object1, targetPosition1);
            MoveObjectToPosition(object2, targetPosition2);
        }
        else if (clickCount == 1)
        {
            DOTween.Sequence()
                .Append(object1.GetComponent<RectTransform>().DOAnchorPos(secondTargetPosition1, 0f))
                .Append(object1.GetComponent<RectTransform>().DOAnchorPos(moveAfterTeleportPositionObject1, moveDuration).SetEase(easeType))
                .Play();
            Debug.Log("MoveAfterTeleport " + moveAfterTeleportPositionObject1);

            MoveObjectToPosition(object2, secondTargetPosition2);
        }
        else if (clickCount == 2)
        {
            DOTween.Sequence()
                .Append(object2.GetComponent<RectTransform>().DOAnchorPos(new Vector3(0, -150f, 0), 0f))
                .Append(object2.GetComponent<RectTransform>().DOAnchorPos(moveAfterTeleportPositionObject2, moveDuration).SetEase(easeType))
                .Play();
            Debug.Log("MoveAfterTeleport " + moveAfterTeleportPositionObject2);

            MoveObjectToPosition(object1, targetPosition1);

            clickCount = 1;
            return;
        }
        clickCount++;
    }

    void MoveObjectToPosition(GameObject obj, Vector3 targetPosition)
    {
        if (obj != null)
        {
            RectTransform rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.DOAnchorPos(new Vector2(rectTransform.anchoredPosition.x, targetPosition.y), moveDuration).SetEase(easeType);
            Debug.Log("Moving " + obj.name + " to " + targetPosition);
        }
    }
}