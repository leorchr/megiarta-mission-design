using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public GameObject questPanelPrefab;
    private GameObject panel;
    public Transform questParent;

    public List<QuestData> questsProgress = new List<QuestData>();
    //private Dictionary<QuestData, GameObject> questVisulization
    //    = new Dictionary<QuestData, GameObject>();

    public void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void Start()
    {
        if(questsProgress.Count > 0)
        {
            panel = Instantiate(questPanelPrefab, questParent);
            panel.GetComponent<QuestPanel>().SetupQuest(questsProgress[0]);
            //questVisulization.Add(questsProgress[0], panel);
        }
        else
        {
            Debug.LogWarning("Il n'y a pas de quête");
        }
    }

    public void TakeQuest(QuestData quest)
    {
        questsProgress.Add(quest);
        panel.GetComponent<QuestPanel>().SetupQuest(quest);
        //questVisulization.Add(quest, panel);
    }

    public void CompleteQuest(QuestData quest)
    {
        questsProgress.Remove(quest);
        panel.GetComponent<QuestPanel>().Complete();
        /*if (questVisulization.ContainsKey(quest))
        {
            Destroy(questVisulization[quest]);
        }*/
    }

    public void Notify()
    {
        /*foreach (GameObject quest in questVisulization.Values)
        {
            QuestPanel panel = quest.GetComponent<QuestPanel>();
            panel.Notify();
        }*/
    }
}