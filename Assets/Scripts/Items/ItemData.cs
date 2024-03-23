using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/Item")]
public class ItemData : ScriptableObject
{
    public Guid UID;
    public string label;
    public Sprite icon;

    private void OnValidate()
    {
        if (UID == Guid.Empty)
        {
            UID = Guid.NewGuid();
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }
    }

    public bool Equals(ItemData data)
    {
        return data != null && data == this;
    }
}