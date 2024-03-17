using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Character",menuName ="Dialogue/Create new Character", order = 11)]
public class CharacterSC : ScriptableObject
{
    public string characterName;
    public Color nameColor;

    public Color defaultColorText;
}
