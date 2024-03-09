using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveBottomArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Rowboat.Instance.quests[Rowboat.Instance.current].currentStep == 2)
        {
            Rowboat.Instance.tag = "Interactive";
            Rowboat.Instance.FinishQuest();
            Destroy(this);
        }
    }
}