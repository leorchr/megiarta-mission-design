using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip islandAmbience;
    [SerializeField] private AudioClip caveAmbience;
    public bool isOnIsland;
    private AudioSource ambienceSource;

    private void Start()
    {
        ambienceSource = GetComponent<AudioSource>();
    }

    public void AmbienceChange()
    {
        if (isOnIsland)
        {
            ambienceSource.clip = caveAmbience;
            ambienceSource.Play();
            isOnIsland = false; 
        }

        else if (!isOnIsland)
        {
            ambienceSource.clip = islandAmbience;
            ambienceSource.Play();
            isOnIsland = true;
        }
    }
}
