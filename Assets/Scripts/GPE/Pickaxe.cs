using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickaxe : Pickable
{
    [SerializeField] private AudioClip pickUp;
    public override void OnPick()
    {
        SFXManager.instance.PlaySound(pickUp);
        if (Rocks.Instance)
        {
            Rocks.Instance.gameObject.tag = "Interactive";
            Rocks.Instance.enabled = true;
        }
        else Debug.LogWarning("Missing rocks for quest 4");
        Rowboat.Instance.FinishQuest();
    }
}
