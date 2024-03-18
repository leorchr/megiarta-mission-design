using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocks : Interactive
{
    public static Rocks Instance;

    public ItemData obsidianPickaxe;

    public List<GameObject> rocks = new List<GameObject>(4);
    private int destroyIndex = 0;
    public GameObject vfx,vfx2;
    public AudioClip RockDestroy;

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

    public override void OnInteraction()
    {
        if (Inventory.Instance.IsItemFound(obsidianPickaxe)){
            if(rocks[destroyIndex] != null)
            {
                GameObject vfxTemp = Instantiate(vfx, this.gameObject.transform);
                vfxTemp.transform.position = rocks[destroyIndex].transform.position;
                SFXManager.instance.PlaySound(RockDestroy);
                GameObject vfxTemp2 = Instantiate(vfx2, this.gameObject.transform);
                vfxTemp2.transform.position = rocks[destroyIndex].transform.position;
                Destroy(rocks[destroyIndex]);
                destroyIndex++;
            }

            if (rocks[destroyIndex+1] != null)
            {
                GameObject vfxTemp = Instantiate(vfx, this.gameObject.transform);
                vfxTemp.transform.position = rocks[destroyIndex+1].transform.position;
                SFXManager.instance.PlaySound(RockDestroy);
                GameObject vfxTemp2 = Instantiate(vfx2, this.gameObject.transform);
                vfxTemp2.transform.position = rocks[destroyIndex+1].transform.position;
                Destroy(rocks[destroyIndex+1]);
                destroyIndex++; ;
            }
        }
        else
        {
            if (rocks[destroyIndex] != null)
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
        if (destroyIndex >= rocks.Count)
        {
            Destroy(colliderRock);
            //Rowboat.Instance.FinishQuest();
            GetComponent<Collider>().isTrigger = true;
            PlayerInteraction.Instance.StopInteractive();
            Destroy(this);
        }
    }
}
