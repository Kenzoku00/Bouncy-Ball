using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public RectTransform[] levelSections; // Array untuk menyimpan RectTransform setiap bagian level
    public float moveDuration = 0.5f; // Durasi pergerakan
    private int currentSectionIndex = 0; // Indeks bagian level yang sedang ditampilkan
    public string mainMenuSceneName = "YonMainMenu";

    public Button nextButton;
    public Button backButton;

    void Start()
    {
        // Set posisi awal dari setiap bagian level
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
        // Pindahkan semua bagian level ke kiri untuk menunjukkan bagian yang diinginkan
        for (int i = 0; i < levelSections.Length; i++)
        {
            float targetX = (i - sectionIndex) * Screen.width;
            levelSections[i].DOAnchorPosX(targetX, moveDuration);
        }

        UpdateButtons();
    }

    void UpdateButtons()
    {
        // Update status tombol Next dan Back
        nextButton.interactable = currentSectionIndex < levelSections.Length - 1;
        backButton.interactable = currentSectionIndex > 0;
    }

    public void OnCloseButton()
    {
        Debug.Log("Sudah di click le");
        SceneManager.LoadScene(0);
    }
}
