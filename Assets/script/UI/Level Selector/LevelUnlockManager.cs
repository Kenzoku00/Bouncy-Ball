using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour
{
    public static LevelUnlockManager Instance;

    [SerializeField] private Button[] levelButtons;
    private int unlockedLevelIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLevelUnlocks();
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

        // Debugging output
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

    // Debugging method
    private void OnEnable()
    {
        Debug.Log("LevelUnlockManager enabled.");
        if (levelButtons != null)
        {
            foreach (var button in levelButtons)
            {
                Debug.Log($"Button assigned: {button.name}");
            }
        }
        else
        {
            Debug.Log("Level buttons array is null.");
        }
    }
}
