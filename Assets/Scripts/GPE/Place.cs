using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{

    public PlaceSC currentPlace;
    private void OnTriggerEnter(Collider other)
    {
        QuestManager.Instance.checkPlace(currentPlace);
    }
}
