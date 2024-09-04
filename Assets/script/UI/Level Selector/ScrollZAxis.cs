using UnityEngine;

public class ScrollZAxisWithBoundsAndTouch : MonoBehaviour
{
    public Transform content; 
    [SerializeField] public float scrollSpeed = 0.5f; 
    [SerializeField]public float touchScrollSpeed = 0.1f; 
    public Vector3 direction = Vector3.forward; 

    public float minZ = -10f; 
    public float maxZ = 10f;  

    private Vector2 touchStartPos;

    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            Vector3 newPosition = content.position + direction * scrollInput * scrollSpeed;
            newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
            content.position = newPosition;
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.position - touchStartPos;
                float touchScrollAmount = touchDelta.y * touchScrollSpeed;

                Vector3 newPosition = content.position + direction * touchScrollAmount;
                newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);
                content.position = newPosition;

                touchStartPos = touch.position; 
            }
        }
    }
}
