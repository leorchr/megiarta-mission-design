using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Pickable
{
    public override void OnPick()
    {
        if (Rocks.Instance)
        {
            Rocks.Instance.gameObject.tag = "Interactive";
            Rocks.Instance.enabled = true;
        }
        else Debug.LogWarning("Missing rocks for quest 4");
        Rowboat.Instance.FinishQuest();
    }
}
