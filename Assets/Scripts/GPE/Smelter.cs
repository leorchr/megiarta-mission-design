using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : QuestInteractor
{
    public static Smelter Instance;

    public ItemData obsidianReward;
    private bool doOnce = false;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    public override void OnInteraction()
    {
        if(!doOnce)
        {
            GiveQuest();
            doOnce = true;
        }
        else if (!waitForObject || Inventory.Instance.HasEveryItem(requiredItems))
        {
            Inventory.Instance.AddToInventory(new QuestItem(obsidianReward, 1));
            FinishQuest();
            Debug.Log("Dialogue fin de quête");
            PlayerInteraction.Instance.StopInteractive();
            Destroy(this);
        }
        else Debug.Log("Dialogue : You must find something");
    }

    public override void GiveQuest()
    {
        if (quests.Count > 0 && current < quests.Count)
        {
            QuestFullData QFD = new QuestFullData(quests[current],this);

            QuestManager.Instance.TakeQuest(QFD, true);

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
}
