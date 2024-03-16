using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public AudioSource audioSource;
    public AudioSource audioQuest;

    public AudioClip missionRewardSound;
    public AudioClip completeStepSound;

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

    public void PlayQuestSound(AudioClip soundToPlay)
    {
        audioQuest.clip = soundToPlay;
        audioQuest.Play();
    }
}
