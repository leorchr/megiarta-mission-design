using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public GameObject questPanelPrefab;
    public Transform questParent;

    public List<QuestData> questsProgress = new List<QuestData>();
    public Dictionary<QuestData, GameObject> questVisulization
        = new Dictionary<QuestData, GameObject>();

    public void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    public void TakeQuest(QuestData quest)
    {
        questsProgress.Add(quest);
        quest.StartQuest();
        GameObject panel = Instantiate(questPanelPrefab, questParent);
        panel.GetComponent<QuestPanel>().SetupQuest(quest);
        questVisulization.Add(quest, panel);
    }

    public void CompleteQuest(QuestData quest)
    {
        Notify(quest.IsFinished());
        questVisulization[quest].GetComponent<QuestPanel>().Complete();
        questsProgress.Remove(quest);
        if (questVisulization.ContainsKey(quest))
        {
            questVisulization.Remove(quest);
        }
    }

    public void Notify(bool finished = false)
    {
        foreach (GameObject quest in questVisulization.Values)
        {
            QuestPanel panel = quest.GetComponent<QuestPanel>();
            if(!finished) panel.Notify();
            else panel.Complete();
        }
    }
}