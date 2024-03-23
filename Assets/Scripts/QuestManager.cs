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
    public List<QuestData> finishedQuest = new List<QuestData>();
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

    public void TakeQuest(QuestData quest)
    {
        questsProgress.Add(quest);
        quest.StartQuest();
        if (quest.isSideQuest)
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
        quest.skipIfSkippable();
    }

    public void CompleteQuest(QuestData quest)
    {
        finishedQuest.Add(quest);
        Wallet.Instance.EarnMoney(quest.moneyReward);
        if (quest.itemReward != null)
        {
            Inventory.Instance.AddToInventory(new QuestItem(quest.itemReward, 1));
            GameManager.instance.NewItem(quest.itemReward);
        }
        Notify(quest,quest.IsFinished());
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

    public bool hasFinishedThisQuest(QuestData quest)
    {
        foreach (QuestData questData in finishedQuest) {
            if (questData == quest)
            {
                return true;
            }
        }
        return false;
    }

    public void Notify( QuestData questTemp ,bool finished = false) 
    {
        if (SFXManager.instance)
        {
            SFXManager.instance.PlayQuestSound(SFXManager.instance.completeStepSound);
        }
        GameObject quest = questVisulization[questTemp];
        QuestPanel panel = quest.GetComponent<QuestPanel>();
        if(!finished) panel.Notify();
        else panel.Complete();
    }

    
    public void CheckItem(QuestItem item)
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.pickItem)
            {
                if (quest.GetCurrentStep().requirements[0].item == item.item)
                {
                    quest.NextStep();
                    GameObject questGO = questVisulization[quest];
                    QuestPanel panel = questGO.GetComponent<QuestPanel>();
                    panel.Notify();
                    return;
                }
            }
        }
    }

    public void checkPlace(PlaceSC place)
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.triggerZone)
            {
                if (quest.GetCurrentStep().placeToVisit == place)
                {
                    quest.NextStep();
                    return;
                }
            }
        }
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

    public void checkReport()
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.Report)
            {
                quest.NextStep();
                return;
            }
        }
    }
    public void checkEventCode(int code)
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.SpecialEvent)
            {
                if (quest.GetCurrentStep().SpecialEventCode == code)
                {
                    quest.NextStep();
                    return;
                }
            }
        }
    }

    public bool isInReport()
    {
        foreach (QuestData quest in questsProgress)
        {
            if (quest.GetCurrentStep().questType == QuestType.Report)
            {
                return true;
            }
        }
        return false;
    }
}