using UnityEngine;

public class PlayerPrefsDebugger : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Current Level: " + PlayerPrefs.GetInt("CurrentLevel", -1));
        Debug.Log("Unlocked Level: " + PlayerPrefs.GetInt("UnlockedLevel", -1));
    }
}
