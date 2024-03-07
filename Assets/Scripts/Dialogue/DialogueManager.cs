using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Canvas UICanvas;
    public GameObject AutomaticDiscussion;
    public GameObject ManualDiscussion;
    public TextMeshProUGUI tm;
    public DialogueSC currentDialogue;
    // Start is called before the first frame update
    void Start()
    {
        PlayDialogue(currentDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayDialogue(DialogueSC dsc)
    {
        if (tm == null)
        {
            
        }
    }
}
