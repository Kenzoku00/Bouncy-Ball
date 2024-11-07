using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneController : MonoBehaviour
{
    [SerializeField] private int maxMoves = 3;
    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private bool moveHorizontally = true;
    [SerializeField] private bool movingRightOrForward = true;
    [SerializeField] private float detectionDistance = 1f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private GameObject objectA;
    [SerializeField] private GameObject object1;
    [SerializeField] private GameObject object2;
    [SerializeField] private GameObject object3;

    private int currentMoves = 0;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    private void Update()
    {
        DetectOverlapBoxes();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && IsTouchingStone(touch.position))
            {
                MoveStone();
            }
        }
    }
        private void DetectOverlapBoxes()
    {
        DetectOverlapBoxForObject(object1);
        DetectOverlapBoxForObject(object2);
        DetectOverlapBoxForObject(object3);
    }
    private void DetectOverlapBoxForObject(GameObject obj)
    {
        Vector3 boxSize = obj.transform.localScale; 
        Vector3 boxCenter = obj.transform.position; 

        Collider[] colliders = Physics.OverlapBox(boxCenter, boxSize / 2, Quaternion.identity);

        if (colliders.Length > 0)
        {
            Debug.Log($"{obj.name} mengalami tumpang tindih dengan {colliders.Length} objek lainnya.");
            foreach (var collider in colliders)
            {
                Debug.Log($"{collider.name} tumpang tindih dengan {obj.name}");
            }
        }
        else
        {
            Debug.Log($"{obj.name} tidak mengalami tumpang tindih.");
        }
    }
    private void MoveStone()
    {
        if (currentMoves < maxMoves)
        {
            float direction = movingRightOrForward ? 1f : -1f;
            if (moveHorizontally)
            {
                targetPosition += new Vector3(direction * moveDistance, 0, 0);
            }
            else
            {
                targetPosition += new Vector3(0, 0, direction * moveDistance);
            }


            currentMoves++;
            Debug.Log($"Move {currentMoves}: Batu bergerak {(movingRightOrForward ? (moveHorizontally ? "ke kanan" : "ke depan") : (moveHorizontally ? "ke kiri" : "ke belakang"))}");
        }
        else
        {
            movingRightOrForward = !movingRightOrForward;
            currentMoves = 1;

            float direction = movingRightOrForward ? 1f : -1f;
            if (moveHorizontally)
            {
                targetPosition += new Vector3(direction * moveDistance, 0, 0);
            }
            else
            {
                targetPosition += new Vector3(0, 0, direction * moveDistance);
            }

            Debug.Log($"Batu berbalik arah: {(movingRightOrForward ? (moveHorizontally ? "ke kanan" : "ke depan") : (moveHorizontally ? "ke kiri" : "ke belakang"))}");
        }

        StartCoroutine(MoveToTargetPosition());
    }

    private IEnumerator MoveToTargetPosition()
    {
    Vector3 initialPositionA = objectA.transform.position;/*
    Vector3 initialPositionB = objectB.transform.position;
    Vector3 initialPositionC = objectC.transform.position;
    Vector3 initialPositionD = objectD.transform.position;*/
    
    Vector3 stoneStartPosition = transform.position;
    
    while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
    {
        float step = Time.deltaTime * 3f;
        transform.position = Vector3.Lerp(stoneStartPosition, targetPosition, step);

        objectA.transform.position = Vector3.Lerp(initialPositionA, targetPosition, step);
        /*objectB.transform.position = Vector3.Lerp(initialPositionB, targetPosition + (objectB.transform.position - stoneStartPosition), step);
        objectC.transform.position = Vector3.Lerp(initialPositionC, targetPosition + (objectC.transform.position - stoneStartPosition), step);
        objectD.transform.position = Vector3.Lerp(initialPositionD, targetPosition + (objectD.transform.position - stoneStartPosition), step);*/

        yield return null;
    }

    transform.position = targetPosition;
    objectA.transform.position = targetPosition;
    /*objectB.transform.position = targetPosition + (objectB.transform.position - stoneStartPosition);
    objectC.transform.position = targetPosition + (objectC.transform.position - stoneStartPosition);
    objectD.transform.position = targetPosition + (objectD.transform.position - stoneStartPosition);*/
    }

    private bool IsTouchingStone(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }
        return false;
    }
}
