using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Canvas UICanvas;
    public GameObject AutomaticDiscussion;
    public GameObject ManualDiscussion;
    private GameObject currentDialogueBox;
    public DialogueSC currentDialogue;

    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayDialogue(currentDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialogue(DialogueSC dsc)
    {
        currentDialogue = dsc;
        if (currentDialogueBox != null)
        {
            Destroy(currentDialogueBox);
            currentDialogueBox = null;
        }
        
        switch(currentDialogue.dialogueType) {
            case DialogueType.Automatic:
                currentDialogueBox = Instantiate(AutomaticDiscussion, UICanvas.transform);
                break;
            case DialogueType.Manual:
                currentDialogueBox = Instantiate(ManualDiscussion, UICanvas.transform);
                break;
            default:  break;
        }

        DialogueBoxContent dbc = currentDialogueBox.GetComponent<DialogueBoxContent>();

        if (dbc.hasSpecificNamePos)
        {
            dbc.nameText.text = dsc.subDialogues[0].getName();
            dbc.contentText.text = dsc.subDialogues[0].getText();
        }
        else
        {
            dbc.contentText.text = dsc.subDialogues[0].getString();
        }
    }
}
