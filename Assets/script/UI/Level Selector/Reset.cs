using UnityEngine;

public class ResetLevelData : MonoBehaviour
{
    public void ResetData()
    {
        // Hapus data level dari PlayerPrefs
        PlayerPrefs.DeleteKey("CurrentLevel");
        PlayerPrefs.DeleteKey("UnlockedLevel");
        PlayerPrefs.Save();

        Debug.Log("Level data has been reset.");
    }
}
