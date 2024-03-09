using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rowboat : QuestInteractor
{
    public static Rowboat Instance;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    private void OnEnable()
    {
        GiveQuest();
    }

    public override void OnInteraction()
    {
        if (quests[current].currentStep == 3)
        {
            Debug.Log("Code Enigme Rowboat");
            Debug.Log("Finish Game");
        }
        else
        {
            Debug.Log("Dialogue if quest not finished");
        }
    }
}
