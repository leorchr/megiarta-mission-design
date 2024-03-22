using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputPrompt : MonoBehaviour
{
    public Sprite KeyboardSprite, ControllerSprite;
    private Image UIImage;

    private void Start()
    {
        UIImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (InputManager.instance.getCurrentControlScheme())
        {
            case ControlScheme.Controller:
                UIImage.sprite = ControllerSprite;
                break;
            case ControlScheme.Keyboard:
                UIImage.sprite = KeyboardSprite;
                break;
            default: break;
        }
    }
}
