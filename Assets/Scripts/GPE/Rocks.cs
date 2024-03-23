using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public struct ItemPower
{
    public ItemData item;
    public float power;
}
public class Rocks : Interactive
{
    public static Rocks Instance;

    public List<ItemPower> itemPowers;

    public List<GameObject> rocks = new List<GameObject>(4);
    private int destroyIndex = 0;
    public GameObject vfx,vfx2;
    public AudioClip RockDestroy;

    public QuestData lockQuestData;
    public int lockQuestStep;

    public int EventCode;

    public GameObject colliderRock;

    private void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }
    private void Start()
    {
        destroyIndex = 0;
    }

    private void Update()
    {
        if (!gameObject.CompareTag("Interactive"))
        {
            foreach (QuestData data in QuestManager.Instance.questsProgress)
            {
                if (data == lockQuestData)
                {
                    if (data.currentStep >= lockQuestStep)
                    {
                        gameObject.tag = "Interactive";
                    }
                }
            }
        }
    }

    public override void OnInteraction()
    {
        foreach (ItemPower ip in itemPowers)
        {
            if (Inventory.Instance.IsItemFound(ip.item))
            {
                for (int i = 0; i < ip.power; i++)
                {
                   DestroyRocks();
                }
            }
        }
        if (destroyIndex >= rocks.Count)
        {
            Destroy(colliderRock);
            QuestManager.Instance.checkEventCode(EventCode);
            GetComponent<Collider>().isTrigger = true;
            PlayerInteraction.Instance.StopInteractive();
            Destroy(this);
        }
    }

    void DestroyRocks()
    {
        if (destroyIndex < rocks.Count && rocks[destroyIndex] != null)
        {
            GameObject vfxTemp = Instantiate(vfx, this.gameObject.transform);
            vfxTemp.transform.position = rocks[destroyIndex].transform.position;
            SFXManager.instance.PlaySound(RockDestroy);
            GameObject vfxTemp2 = Instantiate(vfx2, this.gameObject.transform);
            vfxTemp2.transform.position = rocks[destroyIndex].transform.position;
            Destroy(rocks[destroyIndex]);
            destroyIndex++;
        }
    }
}
