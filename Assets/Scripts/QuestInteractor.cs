using UnityEngine;
using System.Collections.Generic;

public class QuestInteractor : Interactive
{
    public List<QuestData> quests = new List<QuestData>();
    [HideInInspector] public int current = 0;

    public override void OnInteraction()
    {
        Debug.LogWarning("Interaction pas codée");
    }

    public virtual void GiveQuest()
    {
        if (quests.Count > 0 && current < quests.Count)
        {
            QuestManager.Instance.TakeQuest(quests[current]);

            if (quests[current].GetCurrentStep().requirements.Count > 0)
            {
                waitForObject = true;
                //Setting up requirements to finish quests
                foreach (QuestItem item in quests[current].GetCurrentStep().requirements)
                {
                    requiredItems.Add(item);
                }
            }
        }
    }

    public void SetupStep()
    {
        if (quests[current].GetCurrentStep().requirements.Count > 0)
        {
            waitForObject = true;
            //Setting up requirements to finish quests
            foreach (QuestItem item in quests[current].GetCurrentStep().requirements)
            {
                requiredItems.Add(item);
            }
        }
    }

    public virtual void FinishQuest()
    {

        //Dialogue end quest
        if (quests[current].GetCurrentStep().requirements.Count > 0)
        {
            foreach (QuestItem required in quests[current].GetCurrentStep().requirements)
            {
                Inventory.Instance.RemoveFromInventory(required.item, required.quantity);
            }
            requiredItems.Clear();
        }
        //QuestManager.Instance.CompleteQuest(quests[current]);
        quests[current].NextStep();

        waitForObject = false;
        if (!quests[current].IsFinished())
        {
            SetupStep();
            return;
        }
        current++;

        if (current == quests.Count)
        {
            Destroy(this);
        }
        else
        {
            Invoke("GiveQuest", 1f);
        }
    }
}