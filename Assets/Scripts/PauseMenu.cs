using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void QuitSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
