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

    private void Start()
    {
        if(questsProgress.Count > 0)
        {
            TakeQuest(questsProgress[0]);
        }
        else
        {
            Debug.LogWarning("Il n'y a pas de quête");
        }
    }

    public void TakeQuest(QuestData quest)
    {
        questsProgress.Add(quest);
        GameObject panel = Instantiate(questPanelPrefab, questParent);
        panel.GetComponent<QuestPanel>().SetupQuest(quest);
        questVisulization.Add(quest, panel);
    }

    public void CompleteQuest(QuestData quest)
    {
        Notify();
        questsProgress.Remove(quest);
        if (questVisulization.ContainsKey(quest))
        {
            questVisulization.Remove(quest);
        }
    }

    public void Notify()
    {
        foreach (GameObject quest in questVisulization.Values)
        {
            if (quest.GetComponent<QuestPanel>() == null) return;
            QuestPanel panel = quest.GetComponent<QuestPanel>();
            panel.Notify();
        }
    }
}