using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : Interactive
{
    public override void OnInteraction()
    {
        if (Inventory.Instance.IsItemFound(requiredItems[0].item))
        {
            WeirdBranch.Instance.FinishQuest();
        }
        else
        {
            Debug.Log("Dialogue : You must find something");
        }
    }
}
