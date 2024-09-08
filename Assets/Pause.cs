using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;

    public void Pausee()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void Quit()
    {
        SceneManager.LoadScene("Level Selector pangkat dua");
        Time.timeScale = 1;
    }
    public void Resume()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
