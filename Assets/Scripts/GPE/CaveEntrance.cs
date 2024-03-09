using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (Analyser.Instance.current == 2)
        {
            Analyser.Instance.FinishQuest();
            Destroy(this);
        }
    }
}
