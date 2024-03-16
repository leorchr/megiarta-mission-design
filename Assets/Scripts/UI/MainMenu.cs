using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject UiSettings;
    [SerializeField] private GameObject UiMenu;

    [SerializeField] private AudioClip introSound;
    [SerializeField] private AudioSource ambienceSource;

    public void StartGame()
    {
        blackScreen.SetActive(true);
        Invoke("PlayIntroSound", 2f);
    }

    private void PlayIntroSound()
    {
        ambienceSource.clip = introSound;
        ambienceSource.Play();
        Invoke("SceneChange", 40f);
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
