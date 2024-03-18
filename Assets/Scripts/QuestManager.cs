using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public GameObject questPanelPrefab;
    public GameObject sideQuestPanelPrefab;
    public Transform questParent;

    public QuestData startQuest;
    public List<QuestData> questsProgress = new List<QuestData>();
    public Dictionary<QuestData, GameObject> questVisulization
        = new Dictionary<QuestData, GameObject>();


    public void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        if (startQuest) TakeQuest(startQuest);
    }

    public void FinishQuest(QuestData quest)
    {
        DialogueManager.instance.PlayDialogue(quest.GetCurrentStep().EndDialogue);

        if (quest.GetCurrentStep().requirements.Count > 0)
        {
            foreach (QuestItem required in quest.GetCurrentStep().requirements)
            {
                Inventory.Instance.RemoveFromInventory(required.item, required.quantity);
            }
        }
        quest.NextStep();
    }

    public void TakeQuest(QuestData quest, bool isSideQuest = false)
    {
        questsProgress.Add(quest);
        quest.StartQuest();
        if (isSideQuest)
        {
            GameObject panel = Instantiate(sideQuestPanelPrefab, questParent);
            panel.GetComponent<QuestPanel>().SetupQuest(quest);
            questVisulization.Add(quest, panel);
        }
        else
        {
            GameObject panel = Instantiate(questPanelPrefab, questParent);
            panel.GetComponent<QuestPanel>().SetupQuest(quest);
            questVisulization.Add(quest, panel);
        }
    }

    public void CompleteQuest(QuestData quest)
    {
        Wallet.Instance.EarnMoney(quest.moneyReward);
        Notify(quest.IsFinished());
        if(SFXManager.instance)
        {
            SFXManager.instance.PlayQuestSound(SFXManager.instance.missionRewardSound);
        }
        questVisulization[quest].GetComponent<QuestPanel>().Complete();
        questsProgress.Remove(quest);
        if (questVisulization.ContainsKey(quest))
        {
            questVisulization.Remove(quest);
        }
        if (quest.questGivenWhenFinish != null)
        {
            TakeQuest(quest.questGivenWhenFinish);
        }
    }

    public void Notify(bool finished = false) 
    {
        if (SFXManager.instance)
        {
            SFXManager.instance.PlayQuestSound(SFXManager.instance.completeStepSound);
        }
        foreach (GameObject quest in questVisulization.Values)
        {
            QuestPanel panel = quest.GetComponent<QuestPanel>();
            if(!finished) panel.Notify();
            else panel.Complete();
        }
    }

    public void CheckItem(QuestItem item)
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.pickItem)
            {
                if (quest.GetCurrentStep().requirements[0] == item)
                {
                    quest.NextStep();
                    return;
                }
            }
        }
    }

    public void checkPlace(PlaceSC place)
    {
        //TODO : Check if a quest needs to visit a place
    }

    public void checkInteract(QuestGiver QG)
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.interactWithInteractor)
            {
                if (quest.GetCurrentStep().interactorTriger == QG)
                {
                    quest.NextStep();
                    return;
                }
            }
        }
    }
}