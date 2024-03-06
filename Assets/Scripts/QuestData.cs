using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questName;
    [TextArea] public string stepName;

    [TextArea] public string thankYouMessage;

    public List<QuestItem> requirements = new List<QuestItem>();

    public int moneyReward;
}
