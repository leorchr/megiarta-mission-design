using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;

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

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
