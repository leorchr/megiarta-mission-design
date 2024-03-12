using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueBoxContent : MonoBehaviour
{
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI nameText ;

    public bool hasSpecificNamePos;

    public void destroyUI() {  Destroy(gameObject); }
}
