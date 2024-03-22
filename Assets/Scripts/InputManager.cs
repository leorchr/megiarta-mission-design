using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ControlScheme { Controller, Keyboard}
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInput plInput;
    ControlScheme currentControlScheme = ControlScheme.Keyboard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else { Destroy(gameObject); }
    }

    public void updateDevice()
    {
        string s = plInput.currentControlScheme;
        switch (s)
        {
            case "Controller":
                currentControlScheme = ControlScheme.Controller;
                break;
            case "Keyboard":
                currentControlScheme = ControlScheme.Keyboard;
                break;
            default: Debug.LogError("Input Control Scheme Not Found"); break;
        }
    }

    public ControlScheme getCurrentControlScheme() { return currentControlScheme; }
}
