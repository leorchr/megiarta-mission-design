using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingot : Pickable
{
    public override void OnPick()
    {
        Debug.Log("Dialogue");
        Smelter.Instance.FinishQuest();
    }
}
