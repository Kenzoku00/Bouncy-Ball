using UnityEngine;
using DG.Tweening;

public class LevelSelector : MonoBehaviour
{
    public GameObject popupPrefab;  // Assign the popup prefab here
    private GameObject activePopup;

    void Start()
    {
        // Add click handler to each level cube in the scene
        foreach (Transform levelCube in transform)
        {
            // Make sure the level cube has a collider component
            if (levelCube.GetComponent<Collider>() == null)
            {
                levelCube.gameObject.AddComponent<BoxCollider>();
            }

            // Add the click handler script to the level cube
            LevelCubeClickHandler clickHandler = levelCube.gameObject.AddComponent<LevelCubeClickHandler>();
            clickHandler.Initialize(this);
        }
    }

    public void OnLevelCubeClicked(Vector3 position)
    {
        // Destroy any existing popup
        if (activePopup != null)
        {
            Destroy(activePopup);
        }

        // Instantiate a new popup at the clicked cube's position
        activePopup = Instantiate(popupPrefab, position, Quaternion.identity);
        activePopup.transform.SetParent(transform, false);

        // Ensure the popup is active
        activePopup.SetActive(true);

        // Adjust the size and position of the popup if necessary
        RectTransform rectTransform = activePopup.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            // Adjust the size of the popup
            rectTransform.sizeDelta = new Vector2(200, 100); // Set desired width and height
            rectTransform.anchoredPosition = Vector2.zero; // Center the popup
        }

        // Animate the popup
        activePopup.transform.localScale = Vector3.zero;
        activePopup.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

}
