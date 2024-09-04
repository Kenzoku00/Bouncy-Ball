using UnityEngine;

public class LevelCubeClickHandler : MonoBehaviour
{
    private LevelSelector LevelSelector;

    // Initialize the click handler with a reference to the LevelSelector
    public void Initialize(LevelSelector selector)
    {
        LevelSelector = selector;
    }

    // Detect mouse click or touch on the cube
    void OnMouseDown()
    {
        if (LevelSelector != null)
        {
            // Call the method in LevelSelector to handle the click
            LevelSelector.OnLevelCubeClicked(transform.position);
        }
        else
        {
            Debug.LogError("myLevelSelector is not assigned! Make sure Initialize() is called before OnMouseDown.");
        }
    }
}
