using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    private AudioSource audioSource;

    public AudioClip missionRewardSound;

    private void Awake()
    {
        if(instance == null)
            instance = this;    
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip soundToPlay)
    {
        audioSource.clip = soundToPlay;
        audioSource.Play();
    }
}
