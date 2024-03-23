using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{
    public string questName;
    public bool isSideQuest;
    public List<QuestStep> steps = new List<QuestStep>();
    [HideInInspector] public int currentStep = 0;
    [HideInInspector] public QuestInteractor interactor = null;
    public int moneyReward;
    public ItemData itemReward;
    public QuestData questGivenWhenFinish;

    
    public void StartQuest()
    {
        currentStep = 0;
        DialogueManager.instance.PlayDialogue(steps[currentStep].BeginDialogue);
        InteractionHelper.Instance.ShowParticles();
        

    }
    public QuestStep GetCurrentStep()
    {
        return steps[currentStep];
    }

    public void NextStep()
    {
        DialogueManager.instance.PlayDialogue(steps[currentStep].EndDialogue);
        currentStep++;
        if (IsFinished())
        {
            QuestManager.Instance.CompleteQuest(this);
        }
        else
        {
            QuestManager.Instance.Notify(this);
            InteractionHelper.Instance.ShowParticles();
            skipIfSkippable();
        }
    }

    public void skipIfSkippable()
    {
        if (steps[currentStep].skipIf.Count != 0)
        {
            bool skip = true;
            foreach (QuestData q in steps[currentStep].skipIf)
            {
                if (!QuestManager.Instance.hasFinishedThisQuest(q))
                {
                    skip = false; break;
                }
            }
            if (skip) NextStep();
        }
    }

    public bool IsFinished() { 
        return currentStep == steps.Count; 
    }

}

public enum QuestType {pickItem, giveItem, triggerZone, interactWithInteractor, SpecialEvent }

[Serializable]
public class QuestStep
{
    public QuestType questType;

    [TextArea] public string stepName;

    public DialogueSC BeginDialogue;
    public DialogueSC EndDialogue;

    public QuestGiver interactorTriger;

    public List<QuestItem> requirements = new List<QuestItem>();

    public List<QuestData> skipIf = new List<QuestData>();

    public PlaceSC placeToVisit;

    public int SpecialEventCode;
}