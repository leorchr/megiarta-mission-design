using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Transform UiPos;
    public Transform VfxPos;
    public ItemData item;

    public virtual void OnPick()
    {
        QuestManager.Instance.CheckItem(new QuestItem(item, 1));
    }
}