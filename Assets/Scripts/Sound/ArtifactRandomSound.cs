using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactRandomSound : MonoBehaviour
{
    [SerializeField] private List<AudioClip> artifactSounds = new();
    private AudioSource artifactSource;

    private void Start()
    {
        artifactSource = GetComponent<AudioSource>();
    }

    public void MakeSound()
    {
        if (artifactSource != null && artifactSounds.Count != 0)
        {
            AudioClip clip = artifactSounds[Random.Range(0, artifactSounds.Count)];
            artifactSource.clip = clip;
            artifactSource.Play();
        }
    }
}
