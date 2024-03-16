using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSliders : MonoBehaviour
{
    [SerializeField] private AudioMixer ambianceMixer;
    [SerializeField] private AudioMixer sfxAndDialoguesMixer;

    public void SetAmbianceVolume(float sliderValue)
    {
        ambianceMixer.SetFloat("AmbianceMixer", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSfxAndDialoguesVolume(float sliderValue)
    {
        sfxAndDialoguesMixer.SetFloat("SfxAndDialoguesVolume", Mathf.Log10(sliderValue) * 20);
    }
 }
