using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reports : Pickable
{
    [SerializeField] private Analyser analyser;
    public override void OnPick()
    {
        analyser.FinishQuest();
    }
}
