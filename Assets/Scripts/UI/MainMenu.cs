using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject UiSettings;
    [SerializeField] private GameObject UiMenu;
    [SerializeField] private GameObject map;
    [SerializeField] private GameObject exclamation;
    [SerializeField] private GameObject close;

    [SerializeField] private AudioClip introSound;
    [SerializeField] private AudioSource SFXSource;
    [SerializeField] private AudioSource ambienceSource;

    [Header("Controller Support")]
    [SerializeField] private EventSystem inputEventSystem;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject playButton;

    [SerializeField] private DialogueSC Intro;

    [SerializeField] private bool onlyOnce;

    private void Start()
    {
        onlyOnce = false;
    }
    public void StartGame()
    {
        if(onlyOnce) return;
        blackScreen.SetActive(true);
        Invoke("PlayIntroSound", 2f);
        DialogueManager.instance.PlayDialogue(Intro);
        onlyOnce = true;
    }

    private void PlayIntroSound()
    {
        SFXSource.clip = introSound;
        SFXSource.Play();
        ambienceSource.volume = 0.3f;
        Invoke("Map", 5f);
        Invoke("Exclamation", 9f);
        Invoke("Target", 13f);
        Invoke("SceneChange", 37f);
    }

    private void Map()
    {
        map.SetActive(true);
    }

    private void Exclamation()
    {
        exclamation.SetActive(true);
    }
    
    private void Target()
    {
        close.SetActive(true);
    }

    private void SceneChange()
    {
        SceneManager.LoadScene("OfficialLevel");
    }

    public void SettingsMenu()
    {
        if (onlyOnce) return;
        UiSettings.SetActive(true);
        UiMenu.SetActive(false);
        exitButton.GetComponent<Button>().Select();
    }

    public void QuitGame()
    {
        if (onlyOnce) return;
        Application.Quit();
    }

    public void BackToMain()
    {
        UiMenu.SetActive(true);
        UiSettings.SetActive(false);
        playButton.GetComponent<Button>().Select();
    }

    private void Awake()
    {
        Time.timeScale = 1;
    }
}
