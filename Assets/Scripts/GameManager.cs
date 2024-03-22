using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

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
}
