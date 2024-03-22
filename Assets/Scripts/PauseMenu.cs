using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject exitButton;

    public void Resume()
    {
        pauseMenu.SetActive(false);
        if (PlayerController.instance != null)
        {
            PlayerController.instance.unlockPlayer();
        }
        Time.timeScale = 1;
        Cursor.visible = false;
        Debug.Log("Resume Button");
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsPanel.SetActive(true);
        exitButton.GetComponent<Button>().Select();
    }

    public void QuitSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(true);
        settingsButton.GetComponent<Button>().Select();

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
