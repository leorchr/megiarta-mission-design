using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : Interactive
{
    public static Rocks Instance;

    public ItemData obsidianPickaxe;
    private int health = 6;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }
    private void Start(){}

    public override void OnInteraction()
    {
        if (Inventory.Instance.IsItemFound(obsidianPickaxe)){
            health -= 2;
        }
        else
        {
            health -= 1;
        }
        if(health < 1)
        {
            Rowboat.Instance.FinishQuest();
            GetComponent<Collider>().isTrigger = true;
            GetComponent<MeshRenderer>().enabled = false;
            PlayerInteraction.Instance.StopInteractive();
            Destroy(this);
        }
    }
}
