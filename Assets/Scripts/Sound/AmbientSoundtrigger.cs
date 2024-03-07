using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundtrigger : MonoBehaviour
{
    [SerializeField] private AmbientSoundManager ambientSoundManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            print("saucisse");
            ambientSoundManager.AmbienceChange();
        }
    }
}
