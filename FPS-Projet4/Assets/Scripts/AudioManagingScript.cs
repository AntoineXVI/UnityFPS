using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagingScript : MonoBehaviour
{
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    public AudioClip music;
    public AudioClip fire;
    public AudioClip reload;
    public AudioClip ennemyDeath;


    public void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void fireSound()
    {
        sfxSource.clip = fire;
        sfxSource.Play();
    }

    public void reloadSound()
    {
        sfxSource.clip = reload;
        sfxSource.Play();
    }

    public void ennemyDeathSound()
    {
        sfxSource.clip = ennemyDeath;
        sfxSource.Play();
    }
}
