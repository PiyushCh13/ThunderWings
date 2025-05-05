using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource musicSource;
    [SerializeField] private AudioClip musicClip;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.clip = musicClip;

        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }

}
