using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeMessage : Pickable
{
    public override void OnPick()
    {
        Analyser.Instance.FinishQuest();
    }
}
