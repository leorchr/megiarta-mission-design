using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{

    public PlaceSC currentPlace;
    private void OnTriggerEnter(Collider other)
    {
        foreach (QuestData q in QuestManager.Instance.questsProgress)
        {
            
            if (q != null)
            {
                foreach (QuestItem requirement in q.interactor.requiredItems)
                {
                       
                    if (requirement != null)
                    {
                        
                        if (requirement.placeToVisit == currentPlace) {
                            q.interactor.FinishQuest();
                            return;
                        }
                    }
                }
            }
        }
    }
}
