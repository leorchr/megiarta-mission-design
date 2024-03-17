using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue",menuName = "Dialogue/Create New Dialogue", order = 10)]
public class DialogueSC : ScriptableObject
{
    public DialogueType dialogueType;
    public List<SubDialogue> subDialogues;

    
}

[Serializable]
public class SubDialogue
{
    public bool showName;

    public CharacterSC character;
    [TextArea]
    public String text;

    public AudioClip audioClip;

    public String getString() {
        if (showName)
        {
            return getName() + getText();
        }
        else
        {
            return getText();
        }
    }

    public String getName()
    {
        return "<color=#" + ColorUtility.ToHtmlStringRGB(character.nameColor) + ">" + character.characterName + " : </color>";
    }

    public String getText()
    {
        return text;
        //return "<color=#" + ColorUtility.ToHtmlStringRGB(character.defaultColorText) + ">" + text + "</color> ";
    }
}

public enum DialogueType { Automatic, Manual};