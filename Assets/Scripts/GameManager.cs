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
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
    }
}
