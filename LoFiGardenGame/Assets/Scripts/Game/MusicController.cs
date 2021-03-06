﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    public Song[] Songs;

    [HideInInspector]
    public AudioSource AudioSource;

    [HideInInspector]
    public int CurrentSongIndex = 0;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.clip = Songs[CurrentSongIndex].Clip;
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

    public void NextSong()
    {
        CurrentSongIndex = UtilityFunctions.Increment(CurrentSongIndex, Songs.Length - 1);
        UpdateAudioSource();
    }

    public void PreviousSong()
    {
        CurrentSongIndex = UtilityFunctions.Decrement(CurrentSongIndex, Songs.Length - 1);
        UpdateAudioSource();
    }

    public void PlaySong(int index)
    {
        CurrentSongIndex = index;
        UpdateAudioSource();
    }

    private void UpdateAudioSource()
    {
        AudioSource.clip = Songs[CurrentSongIndex].Clip;
        AudioSource.Play();
    }
}
