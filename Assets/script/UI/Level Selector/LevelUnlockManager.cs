using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelUnlockManager : MonoBehaviour
{
    public static LevelUnlockManager Instance {  get; private set; }

    [SerializeField] private Button[] levelButtons;
    private int unlockedLevelIndex = 0;
    private int buttonLength;


    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else {         
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

    }

    private void Start()
    {
        LoadLevelUnlocks();
        UpdateLevelButtons();

        int unlockedLevelIndex = PlayerPrefs.GetInt("UnlockedLevel", 0);
        Debug.Log("Level terakhir yang terbuka adalah: " + (unlockedLevelIndex + 1));

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Level Selector pangkat dua")
        {
            levelButtons = GameObject.Find("LevelButtonsContainer").GetComponentsInChildren<Button>();
            buttonLength = levelButtons.Length;
            UpdateLevelButtons();
        }
        else levelButtons = new Button[0];
    }

    public void UnlockNextLevel()
    {
        int currentLevelIndex = GetLevelIndexFromSceneName(SceneManager.GetActiveScene().name);
        Debug.Log(currentLevelIndex);

        if (currentLevelIndex >= 0 && currentLevelIndex < buttonLength - 1)
        {
            Debug.Log("Kentir");
            unlockedLevelIndex = currentLevelIndex + 1;
            PlayerPrefs.SetInt("UnlockedLevel", unlockedLevelIndex);
            UpdateLevelButtons();
        }
    }

    private int GetLevelIndexFromSceneName(string sceneName)
    {
        if (sceneName.StartsWith("Level "))
        {
            string levelNumberStr = sceneName.Substring("Level ".Length);
            if (int.TryParse(levelNumberStr, out int levelNumber))
            {
                return levelNumber - 1; 
            }
        }
        return -1;
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
