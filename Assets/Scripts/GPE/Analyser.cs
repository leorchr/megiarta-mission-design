using UnityEngine;
using System.Collections.Generic;

public class Analyser : Interactive
{
    public List<QuestData> quests = new List<QuestData>();
    protected int current = 0;

    private void Start()
    {
        GiveQuest();
    }


    public override void OnInteraction()
    {
        //transform.LookAt(Inventory.Instance.transform.position);

        //if (gaveQuest) ThanksMessage();
        /*else if (quests.Count > 0 && current < quests.Count)
        {
            QuestGivingUI.Instance.SetupQuest(quests[current], this);
        }*/

        FinishQuest();
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

    /*void ThanksMessage()
    {
        QuestGivingUI.Instance.ThankYou(quests[current]);
        FinishQuest();
    }*/

    public virtual void FinishQuest()
    {

        //Dialogue end quest
        if(quests[current].GetCurrentStep().requirements.Count > 0)
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
      
        if(current == quests.Count)
        {
            Destroy(this);
        }
        else
        {
            Invoke("GiveQuest", 1f);
        }
    }
}