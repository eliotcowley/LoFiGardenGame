using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void ToggleMusicPlaying()
    {
        if (!AudioSource.isPlaying)
        {
            AudioSource.Play();
        }
        else
        {
            AudioSource.Pause();
        }
    }
}
