using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> grassStepSounds = new();
    [SerializeField] private List<AudioClip> rockStepSounds = new();
    private AudioSource stepSource;

    [SerializeField] private AmbientSoundManager ambientSoundManager;

    void Start()
    {
        stepSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        if(ambientSoundManager.isOnIsland && grassStepSounds.Count != 0)
        {
            AudioClip clip = grassStepSounds[Random.Range(0, grassStepSounds.Count)];
            stepSource.clip = clip;
            stepSource.Play();
        }

        else if(!ambientSoundManager.isOnIsland && rockStepSounds.Count != 0)
        {
            AudioClip clip = rockStepSounds[Random.Range(0, rockStepSounds.Count)];
            stepSource.clip = clip;
            stepSource.Play();
        }
    }
}
