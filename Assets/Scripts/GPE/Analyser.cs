using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Analyser : Interactive
{
    public List<QuestData> quests = new List<QuestData>();
    protected int current = 0;
    private bool firstQuest = false;

    public override void OnInteraction()
    {
        //transform.LookAt(Inventory.Instance.transform.position);

        //if (gaveQuest) ThanksMessage();
        /*else if (quests.Count > 0 && current < quests.Count)
        {
            QuestGivingUI.Instance.SetupQuest(quests[current], this);
        }*/

        if(!firstQuest)
        {
            QuestManager.Instance.CompleteQuest(quests[current]);
            Invoke("GiveQuest",1f);
            firstQuest = true;
        }
        else 
        { 
            FinishQuest();
        }
    }


    public virtual void GiveQuest()
    {
        if (quests.Count > 0 && current < quests.Count)
        {
            if (quests[current].requirements.Count > 0)
            {
                waitForObject = true;
                //Setting up requirements to finish quests
                foreach (QuestItem item in quests[current].requirements)
                {
                    requiredItems.Add(item);
                }
            }
            QuestManager.Instance.TakeQuest(quests[current]);
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
        if(quests[current].requirements.Count > 0)
        {
            foreach (QuestItem required in quests[current].requirements)
            {
                Inventory.Instance.RemoveFromInventory(required.item, required.quantity);
            }
            requiredItems.Clear();
        }
        QuestManager.Instance.CompleteQuest(quests[current]);

        waitForObject = false;

        if (current == quests.Count)
        {
            GetComponent<Collider>().enabled = false;
            Destroy(this);
        }
        else
        {
            Invoke("GiveQuest", 1f);
        }
        current++;
    }
}