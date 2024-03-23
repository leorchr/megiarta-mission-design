using UnityEngine;
using System.Collections.Generic;

public class QuestInteractor : Interactive
{
    public QuestGiver QGInfo;

    public override void OnInteraction()
    {
        QuestManager.Instance.checkInteract(QGInfo);

        bool questGiven = QGInfo.GiveNextQuest();
        if (questGiven)
        {
            DialogueSC d = QGInfo.currentQuest.GetCurrentStep().BeginDialogue;
            if (d != null ) { 
                DialogueManager.instance.PlayDialogue(d);
            }
        }
        else
        {
            if (QGInfo.currentQuest != null)
            {
                if (!QGInfo.currentQuest.IsFinished())
                {
                    DialogueSC currentQDialogue = QGInfo.currentQuest.GetCurrentStep().BeginDialogue;
                    if (currentQDialogue != null) { DialogueManager.instance.PlayDialogue(currentQDialogue); }
                    else { DialogueManager.instance.PlayDialogue(QGInfo.defaultDialogue); }
                }
                else
                {
                    DialogueManager.instance.PlayDialogue(QGInfo.defaultDialogue);
                }
                
            }
            else
            {
                DialogueManager.instance.PlayDialogue(QGInfo.defaultDialogue);
            }
        }
        
       
        
    }

    
}