using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    public Toggle yesToggle;
    public Toggle noToggle;

    public enum enumChoice { None, Yes, No }

    public enumChoice getChoice()
    {
        if (yesToggle.isOn) { return enumChoice.No; }
        else if (noToggle.isOn) { return enumChoice.Yes; }
        return enumChoice.None;
    }

    public void toggleValueChanged(Toggle tg)
    {
        bool isYesToggle = tg == yesToggle;
        if (isYesToggle) {
           if (noToggle.isOn && yesToggle.isOn) { noToggle.isOn = false; }         
        }
        else
        {
            if (yesToggle.isOn && noToggle.isOn) { yesToggle.isOn = false;}
        }
    }

    public void resetToggle()
    {
        yesToggle.isOn = false;
        noToggle.isOn = false;  
    }
}
