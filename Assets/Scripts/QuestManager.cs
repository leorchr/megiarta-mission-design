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

    public List<QuestFullData> questsProgress = new List<QuestFullData>();
    public Dictionary<QuestData, GameObject> questVisulization
        = new Dictionary<QuestData, GameObject>();

    public void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    public void TakeQuest(QuestFullData quest, bool isSideQuest = false)
    {
        questsProgress.Add(quest);
        quest.questData.StartQuest();
        if (isSideQuest)
        {
            GameObject panel = Instantiate(sideQuestPanelPrefab, questParent);
            panel.GetComponent<QuestPanel>().SetupQuest(quest.questData);
            questVisulization.Add(quest.questData, panel);
        }
        else
        {
            GameObject panel = Instantiate(questPanelPrefab, questParent);
            panel.GetComponent<QuestPanel>().SetupQuest(quest.questData);
            questVisulization.Add(quest.questData, panel);
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
        questsProgress.Remove(findQuestFullData(quest));
        if (questVisulization.ContainsKey(quest))
        {
            questVisulization.Remove(quest);
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

    QuestFullData findQuestFullData(QuestData quest) {
        foreach(QuestFullData qfd in questsProgress)
        {
            if (qfd.questData == quest)
            {
                return qfd;
            }
        }
        return null;
    }
}

public class QuestFullData
{
    public QuestFullData(QuestData questP,QuestInteractor interactorP)
    {
        questData = questP;
        interactor = interactorP;
    }

    public QuestData questData;
    public QuestInteractor interactor;
}