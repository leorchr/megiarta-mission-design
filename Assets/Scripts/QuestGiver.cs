using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "Quest System/ Quest Giver",order = 11)]
public class QuestGiver : ScriptableObject
{
    public CharacterSC characterInfo;

    public DialogueSC defaultDialogue;

    public QuestData currentQuest;

    private int currentQuestID = 0;

    public List<QuestData> questToGive;

    public int GetCurrentQuestID() { return currentQuestID; }

    public bool GiveNextQuest() {
        if (currentQuest == null && currentQuestID < questToGive.Count)
        {
            QuestManager.Instance.TakeQuest(questToGive[currentQuestID]);
            return true;
        }
        return false;
    }

    public void FinishQuest()
    {
        currentQuest = null;
        currentQuestID++;
        GiveNextQuest();
    }
}
