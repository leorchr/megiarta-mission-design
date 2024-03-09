using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Pickable
{
    public override void OnPick()
    {
        Rowboat.Instance.FinishQuest();
    }
}
