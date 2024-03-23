using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private GameObject newItemPanel;
    [SerializeField] private Image itemImage;

    private void Start()
    {
        if(instance == null)
            instance = this;
    }

    public void OpenPause(InputAction.CallbackContext callbackContext)
    {
        if(!DialogueManager.instance.isOnDialogue())
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            Cursor.visible = true;
            PlayerController.instance.lockPlayer();
        }
    }

    public void NewItem(ItemData newItem)
    {
        print("she");
        newItemPanel.SetActive(true);
        itemImage.sprite = newItem.icon;
        itemName.text = newItem.name;
        Invoke("newItemPanel", 4f);
    }

    public void Despawn()
    {
        newItemPanel.SetActive(false);
    }
}
