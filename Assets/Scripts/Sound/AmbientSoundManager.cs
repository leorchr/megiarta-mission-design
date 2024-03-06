using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundManager : MonoBehaviour
{
    public static AmbientSoundManager instance;

    [SerializeField] private AudioClip islandAmbience;
    [SerializeField] private AudioClip caveAmbience;
    private AudioSource ambienceSource;

    private void Start()
    {
        ambienceSource = GetComponent<AudioSource>();
    }

    public void AmbienceChange()
    {
        if (ambienceSource.clip = islandAmbience)
            ambienceSource.clip = caveAmbience;

        else if (ambienceSource.clip = caveAmbience)
            ambienceSource.clip = islandAmbience;
    }
}
