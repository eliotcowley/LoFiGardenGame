using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ParticleSystem))]
public class RadioInteraction : Interaction
{
    private AudioSource audioSource;
    private ParticleSystem ps;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ps = GetComponent<ParticleSystem>();
    }

    public override void Interact()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            ps.Play();
        }
        else
        {
            audioSource.Pause();
            ps.Stop();
        }
    }
}
