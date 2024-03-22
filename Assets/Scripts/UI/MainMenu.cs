using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject UiSettings;
    [SerializeField] private GameObject UiMenu;

    [SerializeField] private AudioClip introSound;
    [SerializeField] private AudioSource ambienceSource;

    [Header("Controller Support")]
    [SerializeField] private EventSystem inputEventSystem;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject playButton;

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
        exitButton.GetComponent<Button>().Select();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        UiMenu.SetActive(true);
        UiSettings.SetActive(false);
        playButton.GetComponent<Button>().Select();
    }
}
