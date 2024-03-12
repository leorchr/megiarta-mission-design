using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public DialogueSC startDialogue;
    public Canvas UICanvas;
    public GameObject AutomaticDiscussion;
    public GameObject ManualDiscussion;
    private GameObject currentDialogueBox;
    public DialogueSC currentDialogue;

    public float autoDialogueSpeed = 1;
    private float averageReadingSpeed = 20;

    private int currentSubdialogueID = 0;

    private float timeToNext;

    public List<DialogueSC> dialogueQueue;

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
        PlayDialogue(startDialogue);
        PlayDialogue(startDialogue);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentDialogue != null && currentDialogue.dialogueType == DialogueType.Automatic) {
            timeToNext = Mathf.Clamp(timeToNext - Time.deltaTime, 0, Mathf.Infinity);
            if (timeToNext == 0)
            {
                nextText();
            }
        }
    }

    public void PlayDialogue(DialogueSC dsc)
    {
        if (currentDialogue == null)
        {
            currentDialogue = dsc;
        }
        else
        {
            dialogueQueue.Add(dsc);
            return;
        }
       
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
        timeToNext = calculateTimeToRead(dsc.subDialogues[0].getString());
        currentSubdialogueID = 0;
    }

    public float calculateTimeToRead(string st)
    {
        float t = st.Count() / averageReadingSpeed;
        t /= autoDialogueSpeed;
        return t;
    }

    public void OnValidateInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started && currentDialogue != null && currentDialogue.dialogueType == DialogueType.Manual)
        {
            nextText();
        }
    }

    public void nextText()
    {
        if (currentDialogue != null) 
        {
            currentSubdialogueID++;
            if (currentSubdialogueID >= currentDialogue.subDialogues.Count)
            {
                OnDialogueEnd();
            }
            else
            {
                DialogueBoxContent dbc = currentDialogueBox.GetComponent<DialogueBoxContent>();

                if (dbc.hasSpecificNamePos)
                {
                    dbc.nameText.text = currentDialogue.subDialogues[currentSubdialogueID].getName();
                    dbc.contentText.text = currentDialogue.subDialogues[currentSubdialogueID].getText();


                }
                else
                {
                    dbc.contentText.text = currentDialogue.subDialogues[currentSubdialogueID].getString();
                }
                timeToNext = calculateTimeToRead(currentDialogue.subDialogues[currentSubdialogueID].getString());
            }
            
        }
    }
    public void OnDialogueEnd()
    {
        currentDialogue = null;
        if (currentDialogueBox.GetComponent<Animator>() != null)
        {
            currentDialogueBox.GetComponent<Animator>().SetTrigger("Close");
        }
        else
        {
            GameObject.Destroy(currentDialogueBox);
        }
        currentDialogueBox = null;
        currentSubdialogueID = 0;
        Debug.Log("Dialogue Ended");
        if(dialogueQueue.Count > 0)
        {
            PlayDialogue(dialogueQueue.First());
            dialogueQueue.RemoveAt(0);
            //dialogueQueue.RemoveAt(0);
        }
    }
}
