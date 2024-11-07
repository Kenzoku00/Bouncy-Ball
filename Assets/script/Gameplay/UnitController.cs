using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class UnitController : MonoBehaviour
{
    [SerializeField] private CameraControlAndroid cameraControlAndroid;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] public int maxMoves;

    [SerializeField] Vector2Int movementBoundsMin = new Vector2Int(-10, -10);
    [SerializeField] Vector2Int movementBoundsMax = new Vector2Int(10, 10);

    Transform selectedUnit;
    bool unitSelected = false;
    bool isMoving;
    private GameObject currentTile;

    List<Node> path = new List<Node>();

    public GameObject grass;

    GridManager gridManager;
    Pathfinding pathFinder;
    Teleport tp;

    public int moveCount = 0;
    public TextMeshProUGUI textRemainingMoves;
    int remainingMoves;

    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject flag;

    private bool gameWon = false;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinding>();

        if (gridManager == null || pathFinder == null)
        {
            Debug.LogError("GridManager or Pathfinding not found.");
            return;
        }

        updateText();
    }

    void Update()
    {
        if (gridManager == null || pathFinder == null || gameWon)
            return;

        RaycastFunc();

        if (selectedUnit != null && moveCount >= maxMoves && !CheckWinCondition((Vector2Int)gridManager.GetCoordinatesFromPosition(flag.transform.position)))
        {
            TriggerLoseCondition();
        }

        updateText();
    }

    void TriggerWinCondition()
    {
        Debug.Log("You Win!");
        panelWin.SetActive(true);
        gameWon = true;
        cameraControlAndroid.freeLookCamera.enabled = false;

        LevelUnlockManager.Instance.UnlockNextLevel();

        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
        PlayerPrefs.Save();
    }

    void TriggerLoseCondition()
    {
        Debug.Log("You Lose!");
        panelGameOver.SetActive(true);
        gameWon = true;
        cameraControlAndroid.freeLookCamera.enabled = false;
    }

    public void RaycastFunc()
    {
        if (isMoving || gameWon)  
        return; 


        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray ray = Input.GetMouseButtonDown(0) ? Camera.main.ScreenPointToRay(Input.mousePosition) : Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("tile") || hit.transform.CompareTag("tp"))
                {
                    if (unitSelected && !gameWon) 
                    {
                        Vector2Int targetCords = hit.transform.GetComponent<Tile>().cords;
                        currentTile = hit.transform.gameObject;
                        Vector2Int startCords = new Vector2Int((int)selectedUnit.transform.position.x, (int)selectedUnit.transform.position.z) / gridManager.UnityGridSize;

                        if (IsWithinBounds(targetCords))
                        {
                            if (moveCount < maxMoves)
                            {
                                pathFinder.SetNewDestination(startCords, targetCords);
                                RecalculatePath(true);
                                moveCount++;
                                updateText();

                                if (CheckWinCondition((Vector2Int)gridManager.GetCoordinatesFromPosition(flag.transform.position)))
                                {
                                    TriggerWinCondition();
                                }
                            }
                            else
                            {
                                Debug.Log("Move limit reached. No further movements allowed.");
                            }
                        }
                        else
                        {
                            Debug.Log("Target is out of bounds.");
                        }
                    }
                }
                else if (hit.transform.CompareTag("unit"))
                {
                    selectedUnit = hit.transform;
                    unitSelected = true;
                    Debug.Log("Unit was clicked.");
                }
            }
        }
    }

    bool CheckWinCondition(Vector2Int flagCoordinates)
    {
        if (selectedUnit == null)
            return false;

        Vector2Int playerCoordinates = gridManager.GetCoordinatesFromPosition(selectedUnit.position);
        return playerCoordinates == flagCoordinates;
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = resetPath ? pathFinder.StartCords : gridManager.GetCoordinatesFromPosition(transform.position);

        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        if (selectedUnit == null || gameWon)
            yield break;

        isMoving = true; 
        

        for (int i = 1; i < path.Count; i++)
        {
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].cords);
            selectedUnit.LookAt(new Vector3(endPosition.x, selectedUnit.position.y, endPosition.z));

            float travelTime = Vector3.Distance(selectedUnit.position, endPosition) / movementSpeed;

            Tween moveXTween = selectedUnit.DOMoveX(endPosition.x, travelTime).SetEase(Ease.Linear);
            Tween moveZTween = selectedUnit.DOMoveZ(endPosition.z, travelTime).SetEase(Ease.Linear);

            yield return DOTween.Sequence().Join(moveXTween).Join(moveZTween).WaitForCompletion();

            if (CheckWinCondition((Vector2Int)gridManager.GetCoordinatesFromPosition(flag.transform.position)))
            {
                TriggerWinCondition();
                yield break;
            }

            Debug.Log(endPosition);
        }

        try
        {
            Teleport tp = currentTile.GetComponent<Teleport>();
            tp.Teleports(() =>
            {
                Debug.Log("Teleportation complete. Callback executed in a different script.");
            });
        }
        catch
        {
            Debug.Log("Tp habis");
        }

        isMoving = false;
    }

    bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= movementBoundsMin.x && position.x <= movementBoundsMax.x &&
               position.y >= movementBoundsMin.y && position.y <= movementBoundsMax.y;
    }

    public void updateText()
    {
        remainingMoves = maxMoves - moveCount;
        textRemainingMoves.text = remainingMoves.ToString();
    }

    
}
