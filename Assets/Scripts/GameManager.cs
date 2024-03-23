using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private GameObject newItemPanel;
    [SerializeField] private Image itemImage;

    [SerializeField] private GameObject blackEndScreen;
    [SerializeField] private AudioClip outroClip;

    private void Start()
    {
        if(instance == null)
            instance = this;
    }

    public void OpenPause(InputAction.CallbackContext callbackContext)
    {
        if(!DialogueManager.instance.isOnDialogue() && !QuestManager.Instance.isInReport())
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerController.instance.lockPlayer();
        }
    }

    public void NewItem(ItemData newItem)
    {
        print("she");
        newItemPanel.SetActive(true);
        itemImage.sprite = newItem.icon;
        itemName.text = newItem.label;
        Invoke("Despawn", 4f);
    }

    private void Despawn()
    {
        newItemPanel.SetActive(false);
    }

    public void EndGame()
    {
        blackEndScreen.SetActive(true);
        SFXManager.instance.PlaySound(outroClip);
        Invoke("EndGameLoadScene", 36f);
    }

    private void EndGameLoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
