using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public RectTransform[] levelSections; 
    public float moveDuration = 0.5f; 
    private int currentSectionIndex = 0;
    public string mainMenuSceneName = "YonMainMenu";

    public Button nextButton;
    public Button backButton;

    void Start()
    {
        for (int i = 0; i < levelSections.Length; i++)
        {
            levelSections[i].anchoredPosition = new Vector2(Screen.width * i, 0);
        }

        UpdateButtons();
    }

    public void OnNextButton()
    {
        if (currentSectionIndex < levelSections.Length - 1)
        {
            currentSectionIndex++;
            MoveToSection(currentSectionIndex);
        }
    }

    public void OnBackButton()
    {
        if (currentSectionIndex > 0)
        {
            currentSectionIndex--;
            MoveToSection(currentSectionIndex);
        }
    }

    void MoveToSection(int sectionIndex)
    {
        for (int i = 0; i < levelSections.Length; i++)
        {
            float targetX = (i - sectionIndex) * Screen.width;
            levelSections[i].DOAnchorPosX(targetX, moveDuration);
        }

        UpdateButtons();
    }

    void UpdateButtons()
    {
        nextButton.interactable = currentSectionIndex < levelSections.Length - 1;
        backButton.interactable = currentSectionIndex > 0;
    }

    public void OnCloseButton()
    {
        Debug.Log("Sudah di click le");
        SceneManager.LoadScene(0);
    }
}
