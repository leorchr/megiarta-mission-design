using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questName;
    public List<QuestStep> steps = new List<QuestStep>();
    [HideInInspector] public int currentStep = 0;
    public int moneyReward;
    public void StartQuest()
    {
        currentStep = 0;
    }
    public QuestStep GetCurrentStep()
    {
        return steps[currentStep];
    }

    public void NextStep()
    {
        currentStep++;
        if(IsFinished())
        {
            QuestManager.Instance.CompleteQuest(this);
        }
        else
        {
            QuestManager.Instance.Notify();
        }
    }

    public bool IsFinished() { 
        return currentStep == steps.Count; 
    }
}
[Serializable]
public class QuestStep
{
    [TextArea] public string stepName;

    [TextArea] public string dialogue;
    [TextArea] public string thankYouMessage;

    public List<QuestItem> requirements = new List<QuestItem>();
}