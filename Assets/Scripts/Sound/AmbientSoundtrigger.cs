using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundtrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        AmbientSoundManager.instance.AmbienceChange();
    }
}
