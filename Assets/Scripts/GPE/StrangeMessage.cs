using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeMessage : Pickable
{
    [SerializeField] private AudioClip pickUp;
    public override void OnPick()
    {
        SFXManager.instance.PlaySound(pickUp);
        Analyser.Instance.FinishQuest();
    }
}
