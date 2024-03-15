using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject UiSettings;
    [SerializeField] private GameObject UiMenu;

    public void StartGame()
    {
        blackScreen.SetActive(true);
        Invoke("SceneChange", 2f);
    }

    private void SceneChange()
    {
        SceneManager.LoadScene("OfficialLevel");
    }

    public void SettingsMenu()
    {
        UiSettings.SetActive(true);
        UiMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        UiMenu.SetActive(true);
        UiSettings.SetActive(false);
    }
}
