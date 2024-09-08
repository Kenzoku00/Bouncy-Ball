using UnityEngine;
using UnityEngine.UI; // Untuk Button dan ColorBlock
using UnityEngine.SceneManagement; // Untuk SceneManager, Scene, dan LoadSceneMode

public class LevelUnlockManager : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    private int unlockedLevelIndex = 0;

    private void Start()
    {
        Debug.Log("Scene started, checking if game is paused.");
        Debug.Log("Current Time.timeScale: " + Time.timeScale);
        LoadLevelUnlocks();
        UpdateLevelButtons();
    }

    private void OnEnable()
    {
        // Add this if you need to assign buttons on scene load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-assign references to levelButtons if needed
        levelButtons = GameObject.Find("LevelButtonsContainer").GetComponentsInChildren<Button>();
        UpdateLevelButtons();
    }

    public void UnlockNextLevel()
    {
        if (unlockedLevelIndex < levelButtons.Length - 1)
        {
            unlockedLevelIndex++;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevelIndex);
            UpdateLevelButtons();
        }
    }

    private void UpdateLevelButtons()
    {
        if (levelButtons == null || levelButtons.Length == 0)
        {
            Debug.LogWarning("Level buttons not assigned.");
            return;
        }

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i <= unlockedLevelIndex)
            {
                levelButtons[i].interactable = true;
                ColorBlock color = levelButtons[i].colors;
                color.normalColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, 1f);
                levelButtons[i].colors = color;
            }
            else
            {
                levelButtons[i].interactable = false;
                ColorBlock color = levelButtons[i].colors;
                color.normalColor = new Color(color.normalColor.r, color.normalColor.g, color.normalColor.b, 0.5f);
                levelButtons[i].colors = color;
            }
        }

        Debug.Log("Updated level buttons:");
        foreach (var button in levelButtons)
        {
            if (button != null)
            {
                Debug.Log($"Button: {button.name} - Interactable: {button.interactable}");
            }
            else
            {
                Debug.Log("Button reference is null.");
            }
        }
    }

    public void ResetLevelUnlocks()
    {
        unlockedLevelIndex = 0;
        PlayerPrefs.SetInt("UnlockedLevel", unlockedLevelIndex);
        UpdateLevelButtons();
    }

    private void LoadLevelUnlocks()
    {
        unlockedLevelIndex = PlayerPrefs.GetInt("UnlockedLevel", 0);
    }
}
