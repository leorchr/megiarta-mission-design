using UnityEngine;

public class Pickable : MonoBehaviour
{
    public Transform UiPos;
    public ItemData item;

    public virtual void OnPick()
    {
        Debug.LogWarning("No event");
    }
}