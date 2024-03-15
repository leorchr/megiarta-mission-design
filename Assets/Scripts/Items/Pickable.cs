using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Transform UiPos;
    public Transform VfxPos;
    public ItemData item;

    public virtual void OnPick()
    {
        Debug.LogWarning("No event");
    }
}